namespace ET
{
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(GamerComponent))]
    public static class GamerComponentSystem
    {
        public class GamePlayerCommonentAwakeSystem : AwakeSystem<GamerComponent>
        {
            protected override void Awake(GamerComponent self)
            {
            }
        }

        public static void AddPlayer(this GamerComponent self, Gamer player)
        {
            if (self.PlayerIds.Contains(player.PlayerId))
            {
                self.Gamers[player.PlayerId].Dispose();
                self.Gamers[player.PlayerId] = player;
                return;
            }

            self.Gamers.Add(player.PlayerId, player);
            self.PlayerIds.Add(player.PlayerId);
        }

        public static Gamer GetPlayer(this GamerComponent self, int id)
        {
            if (self.PlayerIds.Exists(t => t == id))
            {
                return self.Gamers[id];
            }

            return null;
        }
    }
}