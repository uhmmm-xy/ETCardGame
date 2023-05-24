using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof (Card))]
    public static class CardSystem
    {
        public class CardAwakeSystem: AwakeSystem<Card, int, int>
        {
            protected override void Awake(Card self, int type, int value)
            {
                self.CardType = type;
                self.CardValue = value;
            }
        }
        
        public static List<Card> SortCard(this List<Card> self)
        {
            self = self.OrderBy(obj => obj.CardType).ThenBy(obj => obj.CardValue).ToList();
            return self;
        }

        public static string GetTypeName(this Card self)
        {
            CardConfig cardConfig = CardConfigCategory.Instance.Get(self.CardType);
            return cardConfig.Name;
        }

        public static string GetValueName(this Card self)
        {
            CardConfig cardConfig = CardConfigCategory.Instance.Get(self.CardType);
            return cardConfig.ValueName[self.CardValue];
        }

        public static CardInfo ToInfo(this Card self)
        {
            CardInfo info = new();
            info.Type = self.CardType;
            info.Value = self.CardValue;
            return info;
        }
    }
}