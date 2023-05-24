using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.Gate)]
    [FriendOf(typeof(Account))]
    public class C2G_EnterRoomHandler : AMRpcHandler<C2G_EnterRoom, G2C_EnterRoom>
    {
        protected override async ETTask Run(Session session, C2G_EnterRoom request, G2C_EnterRoom response)
        {
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                return;
            }


            using (session.AddComponent<SessionLockingComponent>())
            {
                int playerId = session.GetComponent<Account>().PlayerId;

                GameConfig config = GameConfigCategory.Instance.Get(request.GameId);

                StartSceneConfig game = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), config.Name);

                M2G_EnterRoom m2GCreateRoom =
                        await ActorMessageSenderComponent.Instance.Call(game.InstanceId,
                            new G2M_EnterRoom() { RoomId = request.RoomId, PlayerId = playerId }) as M2G_EnterRoom;

                if (m2GCreateRoom.Error != ErrorCode.ERR_Success)
                {
                    response.Error = m2GCreateRoom.Error;
                    response.Message = m2GCreateRoom.Message;
                    response.RoomId = m2GCreateRoom.RoomId;
                    return;
                }

                response.RoomId = m2GCreateRoom.RoomId;
            }
        }
    }
}