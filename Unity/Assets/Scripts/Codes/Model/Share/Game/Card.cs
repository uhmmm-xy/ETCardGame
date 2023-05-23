using System;
using System.Collections.Generic;

namespace ET
{
    [ChildOf(typeof (CardComponent))]
    public class Card: Entity, IAwake, IAwake<int, int>, IDestroy
    {
        public int CardType;
        public int CardValue;
    }
    [FriendOf(typeof(Card))]
    public class CardComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card x, Card y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.CardType == y.CardType && x.CardValue == y.CardValue;
        }

        public int GetHashCode(Card obj)
        {
            return HashCode.Combine(obj.CardType, obj.CardValue);
        }
    }
}