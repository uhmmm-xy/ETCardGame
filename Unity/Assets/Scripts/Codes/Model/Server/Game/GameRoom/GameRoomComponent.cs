using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GameRoomComponent : Entity,IAwake,IDestroy
    {
        public List<int> GameRoomIds;
        public Dictionary<int, GameRoom> GameRooms;
    }
}