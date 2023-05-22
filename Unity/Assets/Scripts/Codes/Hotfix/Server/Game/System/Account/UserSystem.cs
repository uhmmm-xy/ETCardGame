using System.Linq;
using ET.Server;

namespace ET
{
    [FriendOf(typeof(User))]
    public static class UserSystem
    {
        [FriendOf(typeof(Account))]
        public class UserAwakeSystem : AwakeSystem<User>
        {
            protected override void Awake(User self)
            {
                self.PlayerId = self.GetParent<Account>().PlayerId;
            }
        }

        public static async ETTask Save(this User self)
        {
            var list = await DBManagerComponent.Instance.GetZoneDB(1).Query<User>(d => d.PlayerId.Equals(self.PlayerId));
            if (list != null & list.Count > 0)
            {
                await DBManagerComponent.Instance.GetZoneDB(1).Save(self);   
            }
            else
            {
                self.Gender = (int)Gender.Node;
                self.Glod = 100;
                self.Jewel = 100;
                self.Status = (int)UserStatus.None;
                self.HandImage = "";
                self.RoomNumber = 0;
                await DBManagerComponent.Instance.GetZoneDB(1).Save(self);    
            }
        }
        
        public static async ETTask Init(this User self)
        {
            var list = await DBManagerComponent.Instance.GetZoneDB(1).Query<User>(d => d.PlayerId.Equals(self.PlayerId));
            if (list != null & list.Count > 0)
            {
                User user = list.First();
                self.HandImage = user.HandImage;
                self.Glod = user.Glod;
                self.Gender = user.Gender;
                self.Jewel = user.Jewel;
                self.Status = user.Status;
                self.RoomNumber = user.RoomNumber;
            }
            else
            {
                await self.Save();
            }
        }

        public static UserInfo ToInfo(this User self)
        {
            UserInfo info = new ();
            info.PlayerId = self.PlayerId;
            info.Gender = self.Gender;
            info.HeaderImg = self.HandImage;
            info.Glod = self.Glod;
            info.Jewel = self.Jewel;
            info.Status = self.Status;
            info.RoomNumber = info.RoomNumber;
            return info;
        }

    }
}