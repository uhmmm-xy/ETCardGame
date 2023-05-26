using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOf(typeof (Card))]
    public static class CardHelper
    {
        public static List<CardInfo> CardToCardInfo(List<Card> cards)
        {
            List<CardInfo> infos = new();
            foreach (Card card in cards)
            {
                infos.Add(card.ToInfo());
            }

            return infos;
        }

        public static List<Card> CardInfoToCard(List<CardInfo> cards, GameRoom room)
        {
            List<Card> infos = new();
            if (cards is null)
            {
                return infos;
            }

            var cardInfoAndCount = (from t in cards.Distinct()
                select new { item = t, count = cards.Count(i => i.Type == t.Type && i.Value == t.Value) }).ToList();

            foreach (var item in cardInfoAndCount)
            {
                for (int i = 0; i < item.count; i++)
                {
                    infos.Add(room.GetComponent<CardComponent>().GetCard().Where(x => item.item.Value == x.CardValue && item.item.Type == x.CardType)
                            .ToList()[i]);
                }
            }

            return infos;
        }

        public static List<OpenDealMap> ToOpenDealMap(Dictionary<Card, int> maps)
        {
            List<OpenDealMap> ret = new();
            foreach ((Card card, int type) in maps)
            {
                OpenDealMap map = new OpenDealMap() { Card = card.ToInfo(), OpenType = type };
                ret.Add(map);
            }

            return ret;
        }
    }
}