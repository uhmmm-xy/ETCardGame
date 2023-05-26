using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(GameRoom))]
    public class C2M_GamerReadyHandler : AMActorLocationHandler<Account, C2M_GamerReady>
    {

        protected override async ETTask Run(Account entity, C2M_GamerReady message)
        {
            Gamer gamer = entity.GetParent<Gamer>();

            GameRoom room = entity.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameMessageDoing, gamer.RoomId) )
            {
                gamer.Ready();
            
                room.PlayerReady(gamer.PlayerId);

                RoomSendHelper.SendRoomPlayer(room, new M2C_UpdateRoom());
            }
        }
    }
}