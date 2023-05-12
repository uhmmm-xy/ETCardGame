namespace ET
{
    public static class RoomStatus
    {
        public const int None = 0; //无状态，初创建状态
        public const int Gameing = 1; //游戏中
        public const int Wait = 2; //等待中，玩家掉线or网络中断
        public const int Readying = 3; //玩家准备中玩家已坐满
        public const int Ready = 4; //玩家准备中玩家已坐满
        public const int NotFull = 5; //等地玩家进入
    }
}