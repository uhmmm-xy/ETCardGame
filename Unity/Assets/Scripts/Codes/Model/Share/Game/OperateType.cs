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
}