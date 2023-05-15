using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.Gate)]
    [FriendOf(typeof (Account))]
    public class C2G_CreatedRoomHandler: AMRpcHandler<C2G_CreatedRoom, G2C_CreatedRoom>
    {
        protected override async ETTask Run(Session session, C2G_CreatedRoom request, G2C_CreatedRoom response)
        {
            int playerId = session.GetComponent<Account>().PlayerId;
            GameConfig config = GameConfigCategory.Instance.Get(request.GameId);
            StartSceneConfig game = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), config.Name);

            M2G_CreateRoom m2GCreateRoom =
                    await ActorMessageSenderComponent.Instance.Call(game.InstanceId,
                        new G2M_CreateRoom() { GameId = request.GameId, PlayerId = playerId }) as M2G_CreateRoom;

            response.RoomId = m2GCreateRoom.RoomId;
        }
    }
}