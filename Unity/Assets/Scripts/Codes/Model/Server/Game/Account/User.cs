namespace ET
{
    [ComponentOf(typeof (Account))]
    public class User: Entity, IAwake, IDestroy
    {
        public int PlayerId;
        public int Jewel;
        public int Glod;
        public int Gender;
        public string HeaderImg;
        public int Status;
        public int RoomNumber;
    }

    public enum UserStatus
    {
        None = 0,
        Rooming = 1,
        Playing = 1 << 1,
    }
}