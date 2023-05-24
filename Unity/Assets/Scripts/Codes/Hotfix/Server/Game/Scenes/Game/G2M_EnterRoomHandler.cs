using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(GameRoom))]
    public class G2M_EnterRoomHandler : AMActorRpcHandler<Scene, G2M_EnterRoom, M2G_EnterRoom>
    {
        protected override async ETTask Run(Scene scene, G2M_EnterRoom request, M2G_EnterRoom response)
        {
            await ETTask.CompletedTask;
            GameRoomComponent gameRoomComponent = scene.GetComponent<GameRoomComponent>();
            GamerComponent gamerComponent = scene.GetComponent<GamerComponent>();
            GameRoom room = gameRoomComponent.GetRoom(request.RoomId);
            if (room == null)
            {
                response.Error = ErrorCode.ERR_RoomIsNull;
                response.RoomId = 0;
                response.Message = ErrorMessage.RoomIsNull;
                return;
            }
            
            Gamer gamer = gamerComponent.AddChild<Gamer, int, int>(request.RoomId, request.PlayerId);
            await gamer.Init();

            gamerComponent.AddPlayer(gamer);
            
            room.AddPlayer(gamer.PlayerId);
            response.RoomId = room.RoomId;
            
        }
    }
}