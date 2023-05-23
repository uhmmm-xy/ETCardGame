using ET.Server.EventType;

namespace ET
{
    [Event(SceneType.Game)]
    [FriendOf(typeof(Gamer))]
    public class SendPlayerMessageEvent : AEvent<SendPlayerMessage>
    {
        protected override async ETTask Run(Scene scene, SendPlayerMessage a)
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GameMessageDoing, a.Player.Id))
            {
                a.Player.SendMessage(a.Message);
            }
        }
    }
}