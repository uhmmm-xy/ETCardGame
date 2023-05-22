namespace ET
{
    public static class PlayerStatus
    {
        public const int None = 0; //无状态..刚刚进入房间
        public const int Ready = 1; //准备中
        public const int Playing = 2; //游玩中
        public const int GameOver = 3; //游戏结算
        public const int Waitting = 4; //等待
        public const int Bye = 5; //轮空
        public const int Watch = 6; //旁观
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