using System.Linq;

namespace ET
{
    [FriendOf(typeof (TokenComponent))]
    public static class TokenComponentSystem
    {
        public static void Add(this TokenComponent self, int playerId, string token)
        {
            if (self.TokenDictionary.ContainsKey(playerId))
            {
                self.TokenDictionary[playerId] = token;
                return;
            }

            self.TokenDictionary.Add(playerId, token);
        }

        public static int ExistToken(this TokenComponent self,string token)
        {
            if (self.TokenDictionary.ContainsValue(token))
            {
                return self.TokenDictionary.First(item => item.Value == token).Key;
            }
            return 0;
        }
    }
}