using ET.Server;

namespace ET
{
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(User))]
    [FriendOf(typeof(GameRoom))]
    public static class GamerSystem
    {
        public class GamePlayerAwakeSystem : AwakeSystem<Gamer, int>
        {
            protected override void Awake(Gamer self, int playerId)
            {
                self.PlayerId = playerId;
            }
        }

        public static async ETTask Init(this Gamer self)
        {
            Account account = await AccountHelper.GetAccount(self.PlayerId);
            self.AddComponent(account);
            await account.Init();
            account.AddComponent<MailBoxComponent>();
            await account.AddLocation(LocationType.Game);
        }

        public static bool ChangeRoom(this Gamer self, int roomId)
        {
            if (self.RoomId != 0 && self.RoomId != roomId)
            {
                self.DomainScene().GetComponent<GameRoomComponent>().GetRoom(self.RoomId).Players.Remove(self.PlayerId);
            }

            if (!self.DomainScene().GetComponent<GameRoomComponent>().GetRoom(self.RoomId).AddPlayer(self.PlayerId))
            {
                return false;
            }

            self.RoomId = roomId;
            return true;

        }

        public static void SendMessage(this Gamer self, IActorLocationMessage message)
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
            if ((account = self.GetComponent<Account>()) != null)
            {
                id = account.Id;
            }

            return id;
        }

        public static void Ready(this Gamer self)
        {
            self.Status = self.Status switch
            {
                PlayerStatus.Ready => PlayerStatus.None,
                PlayerStatus.None => PlayerStatus.Ready,
                _ => self.Status
            };
        }

        public static GamerInfo ToInfo(this Gamer self)
        {
            GamerInfo info = new();
            info.PlayerId = self.PlayerId;
            User user = self.GetComponent<Account>().GetComponent<User>();
            info.Hander = user.HeaderImg;
            info.Name = "";
            info.Status = self.Status;
            info.Score = self.Score;
            info.OpenDeal = CardHelper.CardToCardInfo(self.OpenDeal);
            info.OutCards = CardHelper.CardToCardInfo(self.OutCards);
            info.HandCards = CardHelper.CardToCardInfo(self.HandCards);
            return info;
        }
    }
}