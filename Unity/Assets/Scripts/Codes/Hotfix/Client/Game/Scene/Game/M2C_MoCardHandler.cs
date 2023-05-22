using ET.EventType;

namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_MoCardHandler: AMHandler<M2C_MoCard>
    {
        protected override async ETTask Run(Session session, M2C_MoCard message)
        {
            await session.ClientScene().GetComponent<ObjectWait>().Wait<Wait_DealCardEnding>(500);
            await EventSystem.Instance.PublishAsync(session.ClientScene(),
                new MoCard() { Card = message.Card });
        }
    }
}