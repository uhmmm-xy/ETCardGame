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

}