namespace ET
{
    [ComponentOf]
    public class Player : Entity,IAwake<UserInfo>,IDestroy
    {
        public int PlayerId;
        public int Jewel;
        public int Glod;
        public int Gender;
        public string HeaderImg;
        public int Status;
        public int RoomNumber;
    }
}