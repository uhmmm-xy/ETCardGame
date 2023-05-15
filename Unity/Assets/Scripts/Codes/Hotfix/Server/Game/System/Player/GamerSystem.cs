using ET.Server;

namespace ET
{
    [FriendOf(typeof(Gamer))]
    public static class GamerSystem
    {
        public class GamePlayerAwakeSystem : AwakeSystem<Gamer, int, int>
        {
            protected override void Awake(Gamer self, int roomId, int playerId)
            {
                self.RoomId = roomId;
                self.PlayerId = playerId;
                self.Init().Coroutine();
            }
        }

        public static async ETTask Init(this Gamer self)
        {
            Account account = await AccountHelper.GetAccount(self.PlayerId);
            account.AddComponent<MailBoxComponent>();
            self.AddComponent(account);
        }

        public static void SendMessage(this Gamer self, IActorLocationRequest message)
        {
            ActorLocationSenderComponent.Instance.Get(LocationType.Player).Send(self.GetAccountId(), message);
        }
        
        public static async ETTask<IActorResponse> CallMessage(this Gamer self, IActorRequest message)
        {
            IActorResponse response = await ActorLocationSenderComponent.Instance.Get(LocationType.Player).Call(self.GetAccountId(), message);
            return response;
        }

        public static long GetAccountId(this Gamer self)
        {
            long id = 0;
            Account account = null;
            if((account = self.GetComponent<Account>())!=null)
            {
                id = account.Id;
            }
            return id;
        }
    }
}