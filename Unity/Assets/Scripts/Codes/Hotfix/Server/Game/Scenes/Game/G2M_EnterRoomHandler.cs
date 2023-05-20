using System.Linq;
using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof (Gamer))]
    [FriendOf(typeof (GameRoom))]
    [FriendOf(typeof (GamerComponent))]
    public class G2M_EnterRoomHandler: AMActorRpcHandler<Scene, G2M_EnterRoom, M2G_EnterRoom>
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

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameEnterAndQuitRoom, request.PlayerId))
            {
                Gamer gamer = null;

                if (gamerComponent.Gamers.Keys.Contains(request.PlayerId))
                {
                    gamer = gamerComponent.GetPlayer(request.PlayerId);
                }
                else
                {
                    gamer = gamerComponent.AddChild<Gamer, int>(request.PlayerId);
                    await gamer.Init();
                    gamerComponent.AddPlayer(gamer);
                }

                if (!gamer.ChangeRoom(room.RoomId))
                {
                    response.Error = ErrorCode.ERR_RoomEnterFail;
                    response.Message = ErrorMessage.RoomEnterFail;
                    return;
                }

                room.SendOtherPlayer(gamer.PlayerId, new G2M_UpdateRoom());

                response.RoomId = room.RoomId;
            }
        }
    }
}