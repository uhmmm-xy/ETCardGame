using System.Collections.Generic;
using ET.Server;

namespace ET
{
    [FriendOf(typeof(GameRoomComponent))]
    [FriendOf(typeof(GameRoom))]
    public static class GameRoomComponentSystem
    {
        public class GameRoomCommpoentAwakeSystem : AwakeSystem<GameRoomComponent>
        {
            protected override void Awake(GameRoomComponent self)
            {
                self.GameRooms = new Dictionary<int, GameRoom>();
                self.GameRoomIds = new List<int>();
            }
        }

        public class GameRoomCommpoentDestroySystem : DestroySystem<GameRoomComponent>
        {
            protected override void Destroy(GameRoomComponent self)
            {
                if (self.GameRooms.Count > 0)
                {
                    foreach (var dic in self.GameRooms)
                    {
                        dic.Value?.Dispose();
                        self.GameRooms.Remove(dic.Key);
                    }

                    self.GameRooms.Clear();
                }

                if (self.GameRoomIds.Count > 0)
                {
                    self.GameRoomIds.Clear();
                }
            }
        }

        public static async ETTask<GameRoom> CreateRoom(this GameRoomComponent self, int GameId)
        {
            //DBManagerComponent db = self.DomainScene().GetComponent<DBManagerComponent>();
            GameConfig gameConfig = GameConfigCategory.Instance.Get(GameId);
            GameRoom room = self.AddChild<GameRoom, GameConfig, int>(gameConfig, GameRoomHelper.GetGameRoomId(self.GameRoomIds));
            //await db.GetZoneDB(self.DomainZone()).Save(room);
            self.GameRoomIds.Add(room.RoomId);
            self.GameRooms.Add(room.RoomId, room);
            await ETTask.CompletedTask;
            return room;
        }

        public static GameRoom GetRoom(this GameRoomComponent self,int roomId)
        {
            if (self.GameRoomIds.Contains(roomId))
            {
                return self.GameRooms[roomId];
            }

            return null;
        }

    }
}