namespace ET
{
    [FriendOf(typeof (AccountSessionComponent))]
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

        public static void Add(this AccountSessionComponent self, int playerId, long sessionId)
        {
            if (self.AccountSessions.ContainsKey(playerId))
            {
                self.AccountSessions[playerId] = sessionId;
            }
            else
            {
                self.AccountSessions.Add(playerId, sessionId);
            }
        }
    }
}