using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(RoundComponent))]
    public static class RoundComponentSystem
    {
        public class RoundComponentAwakeSystem : AwakeSystem<RoundComponent>
        {
            protected override void Awake(RoundComponent self)
            {
                self.RoundIndex = RoundComponent.StartIndex;
            }
        }
        
        public static void CreateRound(this RoundComponent self, List<Card> cards, List<int> players, int startIndex, int gameType)
        {
            Round round = self.AddChild<Round, List<Card>, List<int>, int, int>(cards, players, startIndex, gameType);
            self.Rounds.Add(round);
            self.RoundIndex++;
        }

        public static void RoundOver(this RoundComponent self)
        {
            // self.GetParent<GameRoom>().NextRound();
        }

        public static void Start(this RoundComponent self)
        {
            self.Rounds[self.RoundIndex].DealCard();
        }
    }
}