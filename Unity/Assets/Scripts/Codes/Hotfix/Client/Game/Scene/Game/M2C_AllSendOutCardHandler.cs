using ET.EventType;

namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_AllSendOutCardHandler : AMHandler<M2C_AllSendOutCard>
    {
        protected override async ETTask Run(Session session, M2C_AllSendOutCard message)
        {
            await EventSystem.Instance.PublishAsync(session.ClientScene(),new ChangeAllOutCard()
            {
                OutCardMaps = message.OutMap
            });
        }
    }
}