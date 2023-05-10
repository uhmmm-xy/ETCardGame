namespace ET
{
    [FriendOf(typeof(AccountSessionComponent))]
    public static class AccountSessionComponentSystem
    {

        public static long Get(this AccountSessionComponent self, int PlayerId)
        {
            if (self.AccountSessions.ContainsKey(PlayerId))
            {
                return self.AccountSessions[PlayerId];
            }

            return 0;
        }

    }
}