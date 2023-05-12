using ET.Server;

namespace ET
{
    [FriendOf(typeof(Gamer))]
    public static class GamerSystem
    {
        public class GamePlayerAwakeSystem : AwakeSystem<Gamer, int, int, long>
        {
            protected override void Awake(Gamer self, int roomId, int playerId, long sessionId)
            {
                self.RoomId = roomId;
                self.PlayerId = playerId;
                self.PlayerSessionId = sessionId;
            }
        }

        public static void SendMessage(this Gamer self, IActorMessage message)
        {
            MessageHelper.SendActor(self.PlayerSessionId, message);
        }
        
        public static async ETTask<IActorResponse> CallMessage(this Gamer self, IActorRequest message)
        {
            
            IActorResponse response = await ActorMessageSenderComponent.Instance.Call(self.PlayerSessionId, message);
            return response;
        }
    }
}