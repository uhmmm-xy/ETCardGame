namespace ET
{
    public static class PlayerSystem
    {
        public class PlayerAwakeSystem : AwakeSystem<Player,UserInfo>
        {
            protected override void Awake(Player self, UserInfo info)
            {
                self.HeaderImg = info.HeaderImg;
                self.RoomNumber = info.RoomNumber;
                self.PlayerId = info.PlayerId;
                self.Glod = info.Glod;
                self.Jewel = info.Jewel;
                self.Status = info.Status;
                self.Gender = info.Gender;
            }
        }
    }
}