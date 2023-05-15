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