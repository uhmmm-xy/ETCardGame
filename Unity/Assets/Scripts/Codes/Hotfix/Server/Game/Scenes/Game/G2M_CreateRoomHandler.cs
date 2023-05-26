using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(GameRoom))]
    [FriendOf(typeof(Gamer))]
    public class G2M_CreateRoomHandler : AMActorRpcHandler<Scene, G2M_CreateRoom, M2G_CreateRoom>
    {
        protected override async ETTask Run(Scene scene, G2M_CreateRoom request, M2G_CreateRoom response)
        {
            GameRoomComponent gameRoomComponent = scene.GetComponent<GameRoomComponent>();
            GamerComponent gamerComponent = scene.GetComponent<GamerComponent>();

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameCreateRoom, scene.InstanceId))
            {
                GameRoom gameRoom = await gameRoomComponent.CreateRoom(request.GameId);

                Gamer gamer = null;

                if ((gamer = gamerComponent.GetPlayer(request.PlayerId)) == null)
                {
                    gamer = gamerComponent.AddChild<Gamer, int>(request.PlayerId);
                    await gamer.Init();
                    gamerComponent.AddPlayer(gamer);
                }

                if (!gamer.ChangeRoom(gameRoom.RoomId))
                {
                    response.Error = ErrorCode.ERR_RoomEnterFail;
                    response.Message = ErrorMessage.RoomEnterFail;
                    return;
                }

                response.RoomId = gameRoom.RoomId;
            }
        }
    }
}