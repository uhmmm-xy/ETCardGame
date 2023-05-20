﻿using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(GameRoom))]
    public class C2M_GamerReadyHandler : AMActorLocationRpcHandler<Account, C2M_GamerReady, M2C_GamerReady>
    {
        protected override async ETTask Run(Account unit, C2M_GamerReady request, M2C_GamerReady response)
        {
            await ETTask.CompletedTask;

            Gamer gamer = unit.GetParent<Gamer>();

            GameRoom room = unit.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);

            room.PlayerReady(gamer.PlayerId);

            if (room.RoomStatus == RoomStatus.Readying)
            {
                room.SendOtherPlayer(gamer.PlayerId, new G2M_UpdateRoom());
            }
            
        }
    }
}