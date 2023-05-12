using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Account)]
    public class G2A_GateSessionHandler : AMActorHandler<Scene,G2A_GateSession>
    {
        protected override async ETTask Run(Scene scene, G2A_GateSession message)
        {
            scene.GetComponent<AccountSessionComponent>().Add(message.PlayerId, message.SessionId);
            await ETTask.CompletedTask;
        }
    }
}