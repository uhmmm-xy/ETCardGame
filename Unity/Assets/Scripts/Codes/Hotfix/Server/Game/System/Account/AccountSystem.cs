using System.Linq;
using ET.Server;

namespace ET
{
    [FriendOf(typeof(Account))]
    public static class AccountSystem
    {
        public class AccountAwakeSystem : AwakeSystem<Account, string>
        {
            protected override void Awake(Account self, string a)
            {
                self.PlayerId = int.Parse(a);
            }
        }
        
        public class AccountAwakeIdSystem : AwakeSystem<Account, int>
        {
            protected override void Awake(Account self, int playerId)
            {
                self.PlayerId = playerId;
            }
        }

        public static async ETTask Init(this Account self)
        {
            await self.AddComponent<User>().Init();
        }

        public static bool IsPlayer(this Account self)
        {
            return self.AccountType == (int)AccountType.Node;
        }
        
        public static async ETTask<Account> Save(this Account self)
        {
            var list = await DBManagerComponent.Instance.GetZoneDB(1).Query<Account>(d => d.PlayerId.Equals(self.PlayerId));
            if (list != null & list.Count > 0)
            {
                self = list.First();
                self.LastLoginTime = TimeHelper.ServerNow();
                await DBManagerComponent.Instance.GetZoneDB(1).Save(self);
            }
            else
            {
                self.CreatedTime = TimeHelper.ServerNow();
                self.AccountType = (int)AccountType.Node;
                await DBManagerComponent.Instance.GetZoneDB(1).Save(self);    
            }

            return self;
        }

        public static int GetPlayerId(this Account self)
        {
            return self.PlayerId;
        }
    }
}