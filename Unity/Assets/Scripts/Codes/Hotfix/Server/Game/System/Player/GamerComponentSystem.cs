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

        public static void AddPlayer(this GamerComponent self, long id, Gamer player)
        {
            self.Gamers.Add(id, player);
            self.PlayerIds.Add(player.PlayerId);
        }

        public static Gamer GetPlayer(this GamerComponent self, long id)
        {
            if (self.PlayerIds.Exists(t => t == id))
            {
                return self.Gamers[id];
            }

            return null;
        }
    }
}