using System.Collections.Generic;
using System.Linq;
using ET.Server.EventType;

namespace ET
{
    [FriendOf(typeof (Round))]
    [FriendOf(typeof (Card))]
    [FriendOf(typeof (Gamer))]
    public static class RoundSystem
    {
        public class RoundAwakeSystem: AwakeSystem<Round, List<Card>, List<int>, int, int>
        {
            protected override void Awake(Round self, List<Card> cards, List<int> players, int startIndex, int gameType)
            {
                self.LibCards = cards;
                self.Players = players;
                self.PlayerIndex = startIndex;
                self.GameType = gameType;
                self.Status = RoundStatus.Next;
                switch (gameType)
                {
                    case GameType.HangZhouMahjong:
                        self.AddComponent<HangZhouMahjong>();
                        break;
                }
            }
        }

        public static void DealCard(this Round self)
        {
            self.Players.ForEach(self.SendCardToPlayer);
            self.RoundStart();
        }

        public static void SendCardToPlayer(this Round self, int playerid)
        {
            Gamer gamer = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerid);
            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    self.GetComponent<HangZhouMahjong>().DealCard(gamer);
                    break;
            }
        }

        public static void RoundStart(this Round self)
        {
            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    self.GetComponent<HangZhouMahjong>().RoundStart();
                    break;
            }
        }

        public static bool IsNowPlayer(this Round self, int player)
        {
            if (player == self.Players[self.PlayerIndex])
            {
                return true;
            }

            return false;
        }

        public static bool IsOperate(this Round self)
        {
            return self.Status == RoundStatus.WaitOperate;
        }

        public static void NextRound(this Round self)
        {
            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    self.GetComponent<HangZhouMahjong>().NextRound();
                    break;
            }
        }

        public static long GetPlayer(this Round self, int? index)
        {
            return self.Players[index ?? self.PlayerIndex];
        }

        public static List<Card> GetLibCards(this Round self)
        {
            return self.LibCards;
        }

        public static void OutCard(this Round self, Card card)
        {
            self.OutCards.Insert(0, card);
            self.Players.ForEach(self.AllSendOutCardMessage);
            self.CheckOperate();
            if (self.Status == RoundStatus.Next)
            {
                self.NextRound();
            }
        }

        public static void AllSendOutCardMessage(this Round self, int playerid)
        {
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerid);
            EventSystem.Instance.Publish(self.DomainScene(),
                new SendPlayerMessage()
                {
                    Player = player,
                    Message = new M2C_AllSendOutCard() { OutMap = GameRoomHelper.GetGamerAllOutMap(self.DomainScene(), self.Players) }
                });
            // self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerid)
            //         .SendMessage(new M2C_AllOutCard() { Cards = self.OutCards });
        }

        public static void CheckOperate(this Round self)
        {
            foreach (int player in self.Players.Where(player => player != self.Players[self.PlayerIndex]))
            {
                self.CheckPlayerOperate(player);
            }
        }

        public static void CheckPlayerOperate(this Round self, int playerId)
        {
            bool ret = false;
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerId);
            int operate = 0;
            Card nowOut = self.OutCards.First();
            foreach ((Card card, int type) in player.Operate)
            {
                if (card.CardValue == nowOut.CardValue && card.CardType == nowOut.CardType)
                {
                    operate = self.CheckGamerChiCard(type, player.PlayerId);
                    ret = true;
                    self.Status = RoundStatus.WaitOperate;
                }
            }

            if (ret && operate != OperateType.MahjongNone)
            {
                EventSystem.Instance.Publish(self.DomainScene(),
                    new SendPlayerMessage() { Player = player, Message = new M2C_OperateCard() { OperateType = operate } });
                return;
            }

            self.Status = RoundStatus.Next;
        }

        public static int CheckGamerChiCard(this Round self, int type, int playerId)
        {
            if (self.Players.IndexOf(playerId) == self.PlayerIndex + 1)
            {
                return type;
            }
            
            if (self.Players.IndexOf(playerId) == self.PlayerIndex)
            {
                return OperateType.MahjongNone;
            }

            if (type > OperateType.MahjongPeng)
            {
                return type - OperateType.MahjongChi;
            }

            return OperateType.MahjongNone;
        }
    }
}