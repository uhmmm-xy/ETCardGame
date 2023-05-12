using System.Linq;
using ET.Server;

namespace ET
{
    [FriendOf(typeof(Account))]
    public static class AccountHelper
    {
        public static async ETTask<Account> GetAccount(int playerId)
        {
            Account account = null;
            var list = await DBManagerComponent.Instance.GetZoneDB(1).Query<Account>(d => d.PlayerId.Equals(playerId));
            if (list != null & list.Count > 0)
            {
                account = list.First();
            }
            return account;
        }
    }
}