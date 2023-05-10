using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.Login)]
    public class C2L_LoginAccountHandler: AMRpcHandler<C2L_LoginAccount, L2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2L_LoginAccount request, L2C_LoginAccount response)
        {
            await ETTask.CompletedTask;
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error("Is not Scene!!");
                session?.Disconnect();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (string.IsNullOrEmpty(request.Token))
            {
                response.Error = ErrorCode.ERR_AccountIsNull;
                session?.Disconnect().Coroutine();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_AccountMuchOpt;
                session?.Disconnect().Coroutine();
                return;
            }

            //账号登陆
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.Token.Trim().GetHashCode()))
                {
                    Account account = session.AddChild<Account, string>(request.Token);
                    account = await account.Save();
                    if (!account.IsPlayer())
                    {
                        response.Error = ErrorCode.ERR_AccountNotLogin;
                        session?.Disconnect().Coroutine();
                        return;
                    }

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Account");
                    long loginCenterid = startSceneConfig.InstanceId;
                    A2L_LoginAccount resp =
                            await ActorMessageSenderComponent.Instance.Call(loginCenterid,
                                        new L2A_LoginAccount() { PlayerId = account.GetPlayerId() })
                                    as A2L_LoginAccount;

                    if (resp.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = resp.Error;
                        session?.Disconnect();
                        account?.Dispose();
                        return;
                    }

                    //session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                    //string token = TimeHelper.ServerNow().ToString() + RandomGenerator.RandomNumber(int.MinValue, int.MaxValue);

                    //session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, token);
                    response.Token = resp.Token;
                    response.GateIPAddress = resp.Gate;
                    account?.Dispose();
                }
            }
        }
    }
}