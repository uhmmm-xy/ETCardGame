using System.Collections.Generic;

namespace ET
{
    public static class RoundComponentSystem
    {
        public static Round CreateRound(this RoundComponent self, List<Card> cards, List<long> players, int startIndex,int gameType)
        {
            Round round = self.AddChild<Round, List<Card>, List<long>, int,int>(cards, players, startIndex,gameType);
            return round;
        }
    }
}