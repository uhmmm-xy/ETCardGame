using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(GameRoom))]
    public class G2M_CreateRoomHandler : AMActorRpcHandler<Scene, G2M_CreateRoom, M2G_CreateRoom>
    {
        protected override async ETTask Run(Scene scene, G2M_CreateRoom request, M2G_CreateRoom response)
        {
            GameRoomComponent gameRoomComponent = scene.GetComponent<GameRoomComponent>();
            GamerComponent gamerComponent = scene.GetComponent<GamerComponent>();

            GameRoom gameRoom = await gameRoomComponent.CreateRoom(request.GameId);

            Gamer gamer = gamerComponent.AddChild<Gamer, int, int>(gameRoom.RoomId, request.PlayerId);
            
            await gamer.GetComponent<Account>().AddLocation(LocationType.Game);
            
            gamerComponent.AddPlayer(gamer);

            response.RoomId = gameRoom.RoomId;
        }
    }
}