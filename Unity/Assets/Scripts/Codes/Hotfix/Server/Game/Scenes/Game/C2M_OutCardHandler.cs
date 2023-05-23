using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof (Gamer))]
    public class C2M_OutCardHandler: AMActorLocationHandler<Account, C2M_OutCard>
    {
        protected override async ETTask Run(Account unit, C2M_OutCard request)
        {
            Gamer gamer = unit.GetParent<Gamer>();
            GameRoom room = unit.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);
            if (!room.GetNowRound().IsNowPlayer(gamer.PlayerId) || room.GetNowRound().IsOperate())
            {
                return;
            }

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameMessageDoing, gamer.PlayerId))
            {
                Card card = gamer.OutCard(request.Card.ToEnity());
                if (card is null)
                {
                    return;
                }

                room.GetNowRound().OutCard(card);
                RoomSendHelper.SendRoomPlayer(room, new M2C_UpdateRoom());
            }
        }
    }
}