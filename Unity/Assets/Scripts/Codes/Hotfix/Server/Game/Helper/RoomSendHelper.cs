using System;
using System.Linq;
using ET.Server.EventType;

namespace ET
{
    [FriendOf(typeof(GameRoom))]
    public static class RoomSendHelper
    {
        public static void SendOtherPlayer(GameRoom room, int playerId, IActorLocationMessage message)
        {
            foreach (int other in room.Players.Where(item => item != playerId))
            {
                Gamer gamer = room.DomainScene().GetComponent<GamerComponent>().GetPlayer(other);
                EventSystem.Instance.Publish(room.DomainScene(),
                    new SendPlayerMessage() { Player = gamer, Message = message });
            }
        }


        public static async ETTask CallOtherPlayer(GameRoom room, int playerId, IActorLocationRequest request, Action<IActorResponse> action)
        {
            foreach (int other in room.Players.Where(item => item != playerId))
            {
                IActorResponse response = await room.DomainScene().GetComponent<GamerComponent>().GetPlayer(other).CallMessage(request);
                action(response);
            }
            
        }
        
        public static void SendRoomPlayer(GameRoom room, IActorLocationMessage message)
        {
            foreach (Gamer gamer in room.Players.Select(player => room.DomainScene().GetComponent<GamerComponent>().GetPlayer(player)))
            {
                EventSystem.Instance.Publish(room.DomainScene(),
                    new SendPlayerMessage() { Player = gamer, Message = message });
            }
        }
        
        public static async ETTask CallRoomPlayer(GameRoom room, IActorLocationRequest request, Action<IActorResponse> action)
        {
            foreach (int player in room.Players)
            {
                IActorResponse response = await room.DomainScene().GetComponent<GamerComponent>().GetPlayer(player).CallMessage(request);
                action(response);
            }
        }
    }
}