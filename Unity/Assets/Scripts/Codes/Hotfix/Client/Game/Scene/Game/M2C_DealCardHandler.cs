using ET.EventType;

namespace ET.Client
{
    
    [MessageHandler(SceneType.Client)]
    public class M2C_DealCardHandler : AMHandler<M2C_DealCard>
    {
        protected override async ETTask Run(Session session, M2C_DealCard message)
        {
            await session.ClientScene().GetComponent<ObjectWait>().Wait<Wait_GameStart>(500);
            await EventSystem.Instance.PublishAsync(session.ClientScene(),
                new DealCard() { Cards = message.Cards });
            session.ClientScene().GetComponent<ObjectWait>().Notify(new Wait_DealCardEnding());
        }
    }
}