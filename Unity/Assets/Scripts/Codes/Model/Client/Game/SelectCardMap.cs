namespace ET.Client
{
    public struct SelectCardMap
    {
        public CardInfo Card;
        public int Type;
    }

    public struct SelectCardType
    {
        public const int MahjongChiLeft = 1;
        public const int MahjongChiMiddle = 2;
        public const int MahjongChiReight = 3;
        public const int MahjongGang = 4;
    }
}