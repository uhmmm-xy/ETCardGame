namespace ET
{
    [FriendOf(typeof(Account))]
    public static class AccountSystem
    {
        public class AccountAwakeSystem : AwakeSystem<Account, string>
        {
            protected override void Awake(Account self, string a)
            {
                self.AccountType = (int)AccountType.Node;
                self.ChannelId = 1;
                self.LoginType = (int)LoginType.Test;
                self.PlayerId = 1000;
            }
        }

        public static bool IsPlayer(this Account self)
        {
            return self.AccountType == (int)AccountType.Node;
        }
    }
}