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
            public const int MinRoomId = 1;
            public const int MaxRoomId = 5;
        }
    }
}