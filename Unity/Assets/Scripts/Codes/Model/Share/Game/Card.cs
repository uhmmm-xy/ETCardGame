namespace ET
{
    [ChildOf(typeof (CardComponent))]
    public class Card: Entity, IAwake, IAwake<int, int>, IDestroy
    {
        public int CardType;
        public int CardValue;
    }
}