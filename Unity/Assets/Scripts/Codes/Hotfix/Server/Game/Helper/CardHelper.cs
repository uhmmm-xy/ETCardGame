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
    }
}