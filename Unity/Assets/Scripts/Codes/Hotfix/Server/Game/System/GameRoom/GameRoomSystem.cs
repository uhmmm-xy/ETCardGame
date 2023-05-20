using System;
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
    [FriendOf(typeof(RoundComponent))]
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
                self.PlayerCount = 4;
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
               
                self.RemoveComponent<RoundNextTimer>();
                
                Log.Info("Game Begin");
                self.RoomStatus = RoomStatus.Gameing;

                int startIndex = RandomGenerator.RandomNumber(GameRoom.MinIndex, self.Players.Count);
                RoundComponent roundComponent = self.GetComponent<RoundComponent>();
                Round round = roundComponent
                        .CreateRound(self.GetComponent<CardComponent>().ShuffleCard(), self.Players, startIndex, self.GameType);

                self.RoundIndex = roundComponent.RoundIndex;
                
                round.DealCard();
            }
        }

        public static void NextRound(this GameRoom self)
        {
            if (self.RountCount > self.GetComponent<RoundComponent>().Rounds.Count)
            {
                self.RoomStatus = RoomStatus.Readying;
                self.AddComponent<RoundNextTimer>();
                return;
            }

            self.GameOver();
        }

        public static void GameOver(this GameRoom self)
        {
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

        public static bool AddPlayer(this GameRoom self, int playerId)
        {
            if (self.Players.Count == self.PlayerCount)
            {
                Log.Error("player is full");
                return false;
            }

            if (!self.Players.Contains(playerId))
            {
                self.Players.Add(playerId);
            }

            return true;
        }

        public static void AddWatchPlayer(this GameRoom self, int playerId)
        {
            self.WatchPlayers.Add(playerId);
        }

        public static List<int> GetPlayers(this GameRoom self)
        {
            return self.Players;
        }

        public static void PlayerReady(this GameRoom self, int playerId)
        {
            self.RoomStatus = RoomStatus.Readying;

            self.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerId).Ready();

            List<int> readyPlayer = self.Players.Where(id =>
                    self.DomainScene().GetComponent<GamerComponent>().GetPlayer(id).Status == PlayerStatus.Ready).ToList();

            if (self.Players.Count != self.PlayerCount)
            {
                Log.Info($"Player is not count,player count: {self.Players.Count}");
                return;
            }

            if (readyPlayer.Count != self.PlayerCount)
            {
                Log.Info($"All player do not ready,Ready count : {readyPlayer.Count}");
                return;
            }

            self.RoomStatus = RoomStatus.Ready;
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

        public static void SendOtherPlayer(this GameRoom self, int myId, IActorLocationMessage message)
        {
            foreach (int other in self.Players.Where(item => item != myId))
            {
                self.DomainScene().GetComponent<GamerComponent>().GetPlayer(other).SendMessage(message);
            }
        }

        public static async ETTask CallOtherPlayer(this GameRoom self, int myId, IActorLocationRequest message, Action<IActorResponse> action)
        {
            foreach (int other in self.Players.Where(item => item != myId))
            {
                IActorResponse response = await self.DomainScene().GetComponent<GamerComponent>().GetPlayer(other).CallMessage(message);
                action(response);
            }
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
            Log.Info(roomInfo.ToJson() + $"playerCount : {self.Players.Count}");
            return roomInfo;
        }
    }
}