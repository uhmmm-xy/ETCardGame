using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(Gamer))]
    public class C2M_RoomInfoHandler : AMActorLocationRpcHandler<Account, C2M_RoomInfo, M2C_RoomInfo>
    {
        protected override async ETTask Run(Account unit, C2M_RoomInfo request, M2C_RoomInfo response)
        {
            Gamer gamer = unit.GetParent<Gamer>();
            if (gamer.RoomId != request.RoomId)
            {
                //错误的房间
                response.Error = ErrorCode.ERR_RoomIsNull;
                return;
            }

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameMessageDoing, gamer.RoomId))
            {
                Scene game = unit.DomainScene();
                GameRoom gameRoom = game.GetComponent<GameRoomComponent>().GetRoom(request.RoomId);

                response.Info = ProtoHelper.ToInfo(gameRoom,gamer.PlayerId);
            }
            
        }
    }
}