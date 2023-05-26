namespace ET
{
    [ComponentOf(typeof(Round))]
    public class HangZhouMahjong : Entity,IAwake
    {
        public int Piao;
        public bool Gang;
        public const int JokerType = 4;
        public const int JokerValue = 2;
        public int Mult = 1;
        
    }

    public partial struct CardType
    {
        public const int Tiao = 1;
        public const int Tong = 2;
        public const int Wan = 3;
        public const int Feng = 4;
        public const int Jian = 5;
    }

}