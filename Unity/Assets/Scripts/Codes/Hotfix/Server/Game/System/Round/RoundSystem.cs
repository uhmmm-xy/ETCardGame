using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof (Round))]
    [FriendOf(typeof (Card))]
    [FriendOf(typeof (Gamer))]
    public static class RoundSystem
    {
        public class RoundAwakeSystem: AwakeSystem<Round, List<Card>, List<long>, int, int>
        {
            protected override void Awake(Round self, List<Card> cards, List<long> players, int startIndex, int gameType)
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

        public static void SendCardToPlayer(this Round self, long playerid)
        {
            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    //self.GetComponent<HangZhouMahjong>().DealCard(playerid);
                    break;
            }
        }

        public static void RoundStart(this Round self)
        {
            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    //self.GetComponent<HangZhouMahjong>().RoundStart();
                    break;
            }
        }

        public static bool IsNowPlayer(this Round self,Gamer player)
        {
            if (player.Id == self.Players[self.PlayerIndex])
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
                    //self.GetComponent<HangZhouMahjong>().NextRound();
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
            if (self.Status == RoundStatus.Next)
            {
                self.NextRound();
            }
        }

        public static void AllSendOutCardMessage(this Round self, long playerid)
        {
            // self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerid)
            //         .SendMessage(new M2C_AllOutCard() { Cards = self.OutCards });
        }

        public static void CheckOperate(this Round self)
        {
            self.Players.ForEach(self.CheckPlayerOperate);
        }

        public static void CheckPlayerOperate(this Round self, long playerId)
        {
            bool ret = false;
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerId);
            List<int> operate = new List<int>();
            Card nowOut = self.OutCards.First();
            foreach ((Card card,int type) in player.Operate)
            {
                if (card.CardValue == nowOut.CardValue && card.CardType == nowOut.CardType)
                {
                    operate.Add(type);
                    ret = true;
                    self.Status = RoundStatus.WaitOperate;
                }
            }

            if (ret)
            {
                //player.SendMessage(new M2C_OperateCard() { OperateType = operate });
                return;
            }
            self.Status = RoundStatus.Next;
        }
    }
}