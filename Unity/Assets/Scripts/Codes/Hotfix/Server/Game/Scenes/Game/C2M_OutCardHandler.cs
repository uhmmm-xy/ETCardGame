using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Game)]
    [FriendOf(typeof(Gamer))]
    public class C2M_OutCardHandler : AMActorLocationRpcHandler<Account, C2M_OutCard, M2C_OutCard>
    {
        protected override async ETTask Run(Account unit, C2M_OutCard request, M2C_OutCard response)
        {
            Gamer gamer = unit.GetParent<Gamer>();
            GameRoom room = unit.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);
            if (!room.GetNowRound().IsNowPlayer(gamer.PlayerId) || room.GetNowRound().IsOperate())
            {
                response.HandCard = CardHelper.CardToCardInfo(gamer.HandCards);
                return;
            }
            
            Card card = gamer.OutCard(request.Card.ToEnity());
            if (card is null)
            {
                response.HandCard = CardHelper.CardToCardInfo(gamer.HandCards);
                return;
            }

            foreach (Card item in gamer.Operate.Keys)
            {
                Log.Info($"player {gamer.PlayerId} operateCard in Type:{item.GetTypeName()} and Value:{item.GetValueName()}, is operateType {gamer.Operate[item]}");
            }
            room.GetNowRound().OutCard(card);
            response.HandCard =  CardHelper.CardToCardInfo(gamer.HandCards);
            await ETTask.CompletedTask;
            
        }
    }
}