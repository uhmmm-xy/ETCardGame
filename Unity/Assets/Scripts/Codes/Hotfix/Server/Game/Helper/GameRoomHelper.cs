using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof (Gamer))]
    public static class GameRoomHelper
    {
        public static int GetGameRoomId(List<int> Ids)
        {
            int id = RandomGenerator.RandomNumber(GameRoomID.MinRoomId, GameRoomID.MaxRoomId);
            if (Ids.Exists(t => t == id))
            {
                id = GetGameRoomId(Ids);
            }

            return id;
        }

        public static class GameRoomID
        {
            public const int MinRoomId = 0;
            public const int MaxRoomId = 5;
        }

        public static List<OutCardMap> GetGamerAllOutMap(Scene game, List<int> gamers)
        {
            List<OutCardMap> maps = new();
            foreach (int id in gamers)
            {
                Gamer gamer = game.GetComponent<GamerComponent>().GetPlayer(id);
                OutCardMap map = new () { PlayerId = gamer.PlayerId, Outs = CardHelper.CardToCardInfo(gamer.OutCards) };
                maps.Add(map);
            }

            return maps;
        }
    }
}