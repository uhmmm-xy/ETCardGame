namespace ET
{
    public class OperateType
    {
        public const int None = 0;
        public const int Chi = 1;
        public const int Peng = 2;
        public const int ChiAndPeng = 3;
        public const int Gang = 4;
        public const int ChiAndGang = 5;
        public const int PengAndGang = 6;
        public const int ChiAndPengAndGang = 7;
        public const int Hu = 8;
        public const int ChiAndHu = 9;
        public const int PengAndHu = 10;
        public const int ChiAndPengAndHU = 11;
        public const int GangAndHu = 12;
        public const int ChiAndPengAndHu = 13;
        public const int PengAndGangAndHu = 14;
        public const int ChiAndPengAndGangAndHu = 15;
    }
    
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
}