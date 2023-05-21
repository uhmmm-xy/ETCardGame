using System;
using System.Linq;

namespace ET
{
    [FriendOf(typeof(GameRoom))]
    public static class RoomSendHelper
    {
        public static void SendOtherPlayer(GameRoom room, int playerId, IActorLocationMessage message)
        {
            foreach (int other in room.Players.Where(item => item != playerId))
            {
                room.DomainScene().GetComponent<GamerComponent>().GetPlayer(other).SendMessage(message);
            }
        }


        public static async ETTask CallOtherPlayer(GameRoom room, int playerId, IActorLocationRequest request, Action<IActorResponse> action)
        {
            foreach (int other in room.Players.Where(item => item != playerId))
            {
                IActorResponse response = await room.DomainScene().GetComponent<GamerComponent>().GetPlayer(playerId).CallMessage(request);
                action(response);
            }
            
        }
    }
}