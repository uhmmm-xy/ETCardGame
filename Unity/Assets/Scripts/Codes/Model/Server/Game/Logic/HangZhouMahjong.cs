namespace ET
{
    [ComponentOf(typeof(Round))]
    public class HangZhouMahjong : Entity,IAwake
    {
        
    }

    public partial struct CardType
    {
        public const int Tiao = 1;
        public const int Tong = 2;
        public const int Wan = 3;
        public const int Feng = 4;
        public const int Jian = 5;
    }
    
    public partial struct OperateType
    {
        public const int MahjongNone = 0;
        public const int MahjongChi = 1;
        public const int MahjongPeng = 2;
        public const int MahjongChiAndPeng = 3;
        public const int MahjongGang = 4;
        public const int MahjongChiAndGang = 5;
        public const int MahjongPengAndGang = 6;
        public const int MahjongChiAndPengAndGang = 7;
        public const int MahjongHu = 8;
        public const int MahjongChiAndHu = 9;
        public const int MahjongPengAndHu = 10;
        public const int MahjongChiAndPengAndHU = 11;
        public const int MahjongGangAndHu = 12;
        public const int MahjongChiAndPengAndHu = 13;
        public const int MahjongPengAndGangAndHu = 14;
        public const int MahjongChiAndPengAndGangAndHu = 15;
    }

}