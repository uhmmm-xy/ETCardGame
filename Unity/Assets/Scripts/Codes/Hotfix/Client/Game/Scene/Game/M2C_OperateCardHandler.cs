using ET.EventType;

namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_OperateCardHandler : AMHandler<M2C_OperateCard>
    {
        protected override async ETTask Run(Session session, M2C_OperateCard message)
        {
            await EventSystem.Instance.PublishAsync(session.ClientScene(),
                new GamerOperate() { Type = message.OperateType });
        }
    }
}