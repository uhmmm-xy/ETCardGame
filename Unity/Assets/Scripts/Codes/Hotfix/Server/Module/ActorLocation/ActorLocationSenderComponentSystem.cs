﻿using System;
using System.IO;
using MongoDB.Bson;

namespace ET.Server
{
    [FriendOf(typeof(ActorLocationSenderOneType))]
    [FriendOf(typeof(ActorLocationSender))]
    public static class ActorLocationSenderComponentSystem
    {
        [Invoke(TimerInvokeType.ActorLocationSenderChecker)]
        public class ActorLocationSenderChecker: ATimer<ActorLocationSenderOneType>
        {
            protected override void Run(ActorLocationSenderOneType self)
            {
                try
                {
                    self.Check();
                }
                catch (Exception e)
                {
                    Log.Error($"move timer error: {self.Id}\n{e}");
                }
            }
        }
    
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<ActorLocationSenderOneType, int>
        {
            protected override void Awake(ActorLocationSenderOneType self, int locationType)
            {
                self.LocationType = locationType;
                // 每10s扫描一次过期的actorproxy进行回收,过期时间是2分钟
                // 可能由于bug或者进程挂掉，导致ActorLocationSender发送的消息没有确认，结果无法自动删除，每一分钟清理一次这种ActorLocationSender
                self.CheckTimer = TimerComponent.Instance.NewRepeatedTimer(10 * 1000, TimerInvokeType.ActorLocationSenderChecker, self);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<ActorLocationSenderOneType>
        {
            protected override void Destroy(ActorLocationSenderOneType self)
            {
                TimerComponent.Instance?.Remove(ref self.CheckTimer);
            }
        }
        
        public static void Check(this ActorLocationSenderOneType self)
        {
            using (ListComponent<long> list = ListComponent<long>.Create())
            {
                long timeNow = TimeHelper.ServerNow();
                foreach ((long key, Entity value) in self.Children)
                {
                    ActorLocationSender actorLocationMessageSender = (ActorLocationSender) value;

                    if (timeNow > actorLocationMessageSender.LastSendOrRecvTime + ActorLocationSenderOneType.TIMEOUT_TIME)
                    {
                        list.Add(key);
                    }
                }

                foreach (long id in list)
                {
                    self.Remove(id);
                }
            }
        }

        private static ActorLocationSender GetOrCreate(this ActorLocationSenderOneType self, long id)
        {
            if (id == 0)
            {
                throw new Exception($"actor id is 0");
            }

            if (self.Children.TryGetValue(id, out Entity actorLocationSender))
            {
                return (ActorLocationSender) actorLocationSender;
            }

            actorLocationSender = self.AddChildWithId<ActorLocationSender>(id);
            return (ActorLocationSender) actorLocationSender;
        }

        private static void Remove(this ActorLocationSenderOneType self, long id)
        {
            if (!self.Children.TryGetValue(id, out Entity actorMessageSender))
            {
                return;
            }

            actorMessageSender.Dispose();
        }

        public static void Send(this ActorLocationSenderOneType self, long entityId, IActorRequest message)
        {
            self.Call(entityId, message).Coroutine();
        }

        public static async ETTask<IActorResponse> Call(this ActorLocationSenderOneType self, long entityId, IActorRequest iActorRequest)
        {
            ActorLocationSender actorLocationSender = self.GetOrCreate(entityId);

            // 先序列化好
            int rpcId = ActorMessageSenderComponent.Instance.GetRpcId();
            iActorRequest.RpcId = rpcId;
            
            long actorLocationSenderInstanceId = actorLocationSender.InstanceId;
            int coroutineLockType = (self.LocationType << 16) | CoroutineLockType.ActorLocationSender;
            using (await CoroutineLockComponent.Instance.Wait(coroutineLockType, entityId))
            {
                if (actorLocationSender.InstanceId != actorLocationSenderInstanceId)
                {
                    throw new RpcException(ErrorCore.ERR_ActorTimeout, $"{iActorRequest}");
                }

                // 队列中没处理的消息返回跟上个消息一样的报错
                if (actorLocationSender.Error == ErrorCore.ERR_NotFoundActor)
                {
                    return ActorHelper.CreateResponse(iActorRequest, actorLocationSender.Error);
                }
                
                try
                {
                    return await self.CallInner(actorLocationSender, rpcId, iActorRequest);
                }
                catch (RpcException)
                {
                    self.Remove(actorLocationSender.Id);
                    throw;
                }
                catch (Exception e)
                {
                    self.Remove(actorLocationSender.Id);
                    throw new Exception($"{iActorRequest}", e);
                }
            }
        }

        private static async ETTask<IActorResponse> CallInner(this ActorLocationSenderOneType self, ActorLocationSender actorLocationSender, int rpcId, IActorRequest iActorRequest)
        {
            int failTimes = 0;
            long instanceId = actorLocationSender.InstanceId;
            actorLocationSender.LastSendOrRecvTime = TimeHelper.ServerNow();
            
            while (true)
            {
                if (actorLocationSender.ActorId == 0)
                {
                    actorLocationSender.ActorId = await LocationProxyComponent.Instance.Get(self.LocationType, actorLocationSender.Id);
                    if (actorLocationSender.InstanceId != instanceId)
                    {
                        throw new RpcException(ErrorCore.ERR_ActorLocationSenderTimeout2, $"{iActorRequest}");
                    }
                }

                if (actorLocationSender.ActorId == 0)
                {
                    actorLocationSender.Error = ErrorCore.ERR_NotFoundActor;
                    return ActorHelper.CreateResponse(iActorRequest, ErrorCore.ERR_NotFoundActor);
                }
                IActorResponse response = await ActorMessageSenderComponent.Instance.Call(actorLocationSender.ActorId, rpcId, iActorRequest, false);
                if (actorLocationSender.InstanceId != instanceId)
                {
                    throw new RpcException(ErrorCore.ERR_ActorLocationSenderTimeout3, $"{iActorRequest}");
                }

                switch (response.Error)
                {
                    case ErrorCore.ERR_NotFoundActor:
                    {
                        // 如果没找到Actor,重试
                        ++failTimes;
                        if (failTimes > 20)
                        {
                            Log.Debug($"actor send message fail, actorid: {actorLocationSender.Id}");
                            actorLocationSender.Error = ErrorCore.ERR_NotFoundActor;
                            // 这里不能删除actor，要让后面等待发送的消息也返回ERR_NotFoundActor，直到超时删除
                            return response;
                        }

                        // 等待0.5s再发送
                        await TimerComponent.Instance.WaitAsync(500);
                        if (actorLocationSender.InstanceId != instanceId)
                        {
                            throw new RpcException(ErrorCore.ERR_ActorLocationSenderTimeout4, $"{iActorRequest}");
                        }

                        actorLocationSender.ActorId = 0;
                        continue;
                    }
                    case ErrorCore.ERR_ActorTimeout:
                    {
                        throw new RpcException(response.Error, $"{iActorRequest}");
                    }
                }

                if (ErrorCore.IsRpcNeedThrowException(response.Error))
                {
                    throw new RpcException(response.Error, $"Message: {response.Message} Request: {iActorRequest}");
                }

                return response;
            }
        }
    }

    [FriendOf(typeof (ActorLocationSenderComponent))]
    public static class ActorLocationSenderManagerComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<ActorLocationSenderComponent>
        {
            protected override void Awake(ActorLocationSenderComponent self)
            {
                ActorLocationSenderComponent.Instance = self;
                for (int i = 0; i < self.ActorLocationSenderComponents.Length; ++i)
                {
                    self.ActorLocationSenderComponents[i] = self.AddChild<ActorLocationSenderOneType, int>(i);
                }
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<ActorLocationSenderComponent>
        {
            protected override void Destroy(ActorLocationSenderComponent self)
            {
                ActorLocationSenderComponent.Instance = null;
            }
        }
        
        public static ActorLocationSenderOneType Get(this ActorLocationSenderComponent self, int locationType)
        {
            return self.ActorLocationSenderComponents[locationType];
        }
    }
}