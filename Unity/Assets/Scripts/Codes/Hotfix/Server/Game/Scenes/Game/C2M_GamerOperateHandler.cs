using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof (Gamer))]
    public class C2M_GamerOperateHandler: AMActorLocationHandler<Account, C2M_GamerOperate>
    {
        protected override async ETTask Run(Account entity, C2M_GamerOperate message)
        {
            Gamer gamer = entity.GetParent<Gamer>();
            GameRoom room = entity.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);
            Log.Info($"Gamer {gamer.PlayerId} do Operate {message.Operate}");
            if (!room.GetNowRound().IsOperate())
            {
                Log.Info($"Room is Not Operate");
                return;
            }

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameMessageDoing, gamer.PlayerId))
            {
                room.GetNowRound().Operate(gamer, message.Operate, CardHelper.CardInfoToCard(message.OperateCards));
                RoomSendHelper.SendRoomPlayer(room, new M2C_UpdateRoom());
            }
        }
    }
}