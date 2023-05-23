using System.Collections.Generic;

namespace ET
{
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
        
        public static List<Card> CardInfoToCard(List<CardInfo> cards)
        {
            List<Card> infos = new();
            if (cards is null)
            {
                return infos;
            }
            foreach (CardInfo card in cards)
            {
                infos.Add(card.ToEnity());
            }

            return infos;
        }

        public static List<OpenDealMap> ToOpenDealMap(Dictionary<Card, int> maps)
        {
            List<OpenDealMap> ret = new ();
            foreach ((Card card,int type) in maps)
            {
                OpenDealMap map = new OpenDealMap() { Card = card.ToInfo(), OpenType = type };
                ret.Add(map);
            }
            return ret;
        }
    }
}