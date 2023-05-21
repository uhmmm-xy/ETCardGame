using ET.Server.EventType;

namespace ET
{
    [Event(SceneType.Game)]
    public class SendPlayerMessageEvent : AEvent<SendPlayerMessage>
    {
        protected override async ETTask Run(Scene scene, SendPlayerMessage a)
        {
            await ETTask.CompletedTask;
            a.Player.SendMessage(a.Message);
        }
    }
}