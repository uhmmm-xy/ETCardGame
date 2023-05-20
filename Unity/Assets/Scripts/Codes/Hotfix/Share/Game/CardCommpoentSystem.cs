using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof (CardComponent))]
    [FriendOf(typeof (Card))]
    public static class CardCommpoentSystem
    {
        public class CardCommpoentAwakeSystem: AwakeSystem<CardComponent, int[]>
        {
            protected override void Awake(CardComponent self, int[] cardId)
            {
                self.Init(cardId);
            }
        }

        public static List<Card> ShuffleCard(this CardComponent self)
        {
            List<Card> cpList = self.Cards.ToList();
            RandomGenerator.BreakRank(cpList);
            return cpList;
        }

        public static void Init(this CardComponent self, int[] config)
        {
            foreach (int cardId in config)
            {
                self.CardTypeEach(cardId);
            }
        }

        private static void CardTypeEach(this CardComponent self, int CardId)
        {
            CardConfig cardConfig = CardConfigCategory.Instance.Get(CardId);
            for (int i = cardConfig.MinValue; i <= cardConfig.MaxValue; i++)
            {
                self.CardCreated(i, cardConfig.Id, cardConfig.Count);
            }
        }

        private static void CardCreated(this CardComponent self, int value, int type, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Card card = self.AddChild<Card, int, int>(type, value);
                self.Cards.Add(card);
            }
        }

        public static List<Card> GetCard(this CardComponent self)
        {
            return self.Cards;
        }
    }
}