using System.Collections.Generic;
using System.Linq;

namespace ET
{
    
    public static class GameType
    {
        public const int HangZhouMahjong = 1001;
    }

    [FriendOf(typeof(GameRoom))]
    [FriendOf(typeof(Gamer))]
    public static class GameRoomSystem
    {
        public class GameRoomAkaweSystem : AwakeSystem<GameRoom, GameConfig, int>
        {
            protected override void Awake(GameRoom self, GameConfig config, int RoomId)
            {
                self.Config = config;
                self.RoomId = RoomId;
                self.GameType = config.Id;
                self.RoomStatus = RoomStatus.None;
                self.PlayerCount = 2;
                self.AddComponent<CardComponent, int[]>(self.Config.GameCardType);
                self.AddComponent<RoundComponent>();
            }
        }

        public class GameRoomDestroySystem : DestroySystem<GameRoom>
        {
            protected override void Destroy(GameRoom self)
            {
                self.Players.Clear();
                self.WatchPlayers.Clear();
                self.GetComponent<RoundComponent>()?.Dispose();
                self.GetComponent<CardComponent>()?.Dispose();
                self.Dispose();
            }
        }

        public static void Start(this GameRoom self)
        {
            if (self.RoomStatus == RoomStatus.Ready)
            {
                Log.Info("Game Begin");
                int startIndex = RandomGenerator.RandomNumber(1, self.Players.Count) - 1;
                Round round = self.GetComponent<RoundComponent>()
                        .CreateRound(self.GetComponent<CardComponent>().ShuffleCard(), self.Players, startIndex, self.GameType);
                self.RoundIndex = round.Id;
                round.DealCard();
            }
        }

        public static int GetGameType(this GameRoom self)
        {
            int ret = self.GameType;
            return ret;
        }

        public static int GetRoomId(this GameRoom self)
        {
            return self.RoomId;
        }

        public static Round GetNowRound(this GameRoom self)
        {
            return self.GetComponent<RoundComponent>().GetChild<Round>(self.RoundIndex);
        }

        public static void AddPlayer(this GameRoom self, int playerId)
        {
            if (self.Players.Count == self.PlayerCount)
            {
                Log.Error("player is full");
                return;
            }

            self.Players.Add(playerId);
        }

        public static void AddWatchPlayer(this GameRoom self, int playerId)
        {
            self.WatchPlayers.Add(playerId);
        }

        public static List<int> GetPlayers(this GameRoom self)
        {
            return self.Players;
        }

        public static void PlayerReady(this GameRoom self)
        {
            List<int> readyPlayer = self.Players.Where(playerId =>
                    self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerId).Status == PlayerStatus.Ready).ToList();

            self.SendPlayerReadying(readyPlayer);
            self.RoomStatus = RoomStatus.Readying;
            if (self.Players.Count != self.PlayerCount)
            {
                Log.Info("Player is not count");
                return;
            }

            if (readyPlayer.Count != self.PlayerCount)
            {
                return;
            }

            self.RoomStatus = RoomStatus.Ready;
            self.Players.ForEach(self.SendGameStarting);
            Log.Info("Game is Begin");
            self.Start();
        }

        public static void SendGameStarting(this GameRoom self, int player)
        {
            Gamer gamePlayer = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(player);
            // gamePlayer.SendMessage(new M2C_GameStarting());
        }

        public static void SendPlayerReadying(this GameRoom self, List<int> readyplayer)
        {
            self.Players.ForEach(v =>
            {
                Gamer gamePlayer = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(v);
                //gamePlayer.SendMessage(new M2C_PlayerReadying() { PlayerIds = readyplayer });
            });
        }

        public static RoomInfo ToInfo(this GameRoom self)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.Players = new();
            roomInfo.Rounds = new();
            roomInfo.RoomId = self.RoomId;
            roomInfo.Status = self.RoomStatus;
            foreach (int player in self.Players)
            {
                Gamer gamer = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(player);
                roomInfo.Players.Add(gamer.ToInfo());
            }
            Log.Info(roomInfo.ToJson()+$"playerCount : {self.Players.Count}");
            return roomInfo;
        }
    }
}