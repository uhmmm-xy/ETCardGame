using System.Collections.Generic;

namespace ET
{
    [ChildOf(typeof (GameRoomComponent))]
    public class GameRoom: Entity, IAwake<GameConfig, int>, IDestroy
    {
        public const int MinIndex = 0;
        
        public GameConfig Config; //游戏配置
        public List<int> Players = new(); //玩家集合
        public List<int> WatchPlayers = new(); //旁观玩家集合
        public List<Round> Rounds = new();
        public Dictionary<int, int> PlayConfig = new();

        public int GameType;

        public int PlayerCount; //玩家数
        public int RountCount; //回合数 

        public long RoundIndex; //当前回合
        public int PlayerIndex; //当前玩家

        public int RoomId;

        public int RoomStatus;
    }
}