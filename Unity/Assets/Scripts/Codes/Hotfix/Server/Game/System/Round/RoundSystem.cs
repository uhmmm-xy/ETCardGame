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

        public static bool IsNextPlayer(this Round self, int player)
        {
            return self.Players.IndexOf(player) == (self.PlayerIndex + 1);
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

            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    self.GetComponent<HangZhouMahjong>().OutCard();
                    break;
            }
            if (self.Status == RoundStatus.Next)
            {
                self.NextRound();
            }
        }

        public static void Operate(this Round self,Gamer gamer,int operate,List<Card> operateCards)
        {
            switch (self.GameType)
            {
                case GameType.HangZhouMahjong:
                    self.GetComponent<HangZhouMahjong>().Operate(gamer,operate,operateCards);
                    break;
            }
        }

        public static RoundInfo ToInfo(this Round self)
        {
            RoundInfo info = new RoundInfo();
            info.PlayerIndex = self.PlayerIndex;
            info.Status = (int)self.Status;
            info.Score = self.Score;
            info.OutCard = CardHelper.CardToCardInfo(self.OutCards);
            info.StartIndex = self.StartIndex;
            return info;
        }

    }
}