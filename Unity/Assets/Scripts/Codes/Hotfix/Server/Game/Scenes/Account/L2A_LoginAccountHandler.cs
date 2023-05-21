using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Account)]
    public class L2A_LoginAccountHandler: AMActorRpcHandler<Scene, L2A_LoginAccount, A2L_LoginAccount>
    {
        protected override async ETTask Run(Scene scene, L2A_LoginAccount request, A2L_LoginAccount response)
        {
            long sessionId = 0;
            if ((sessionId = scene.GetComponent<AccountSessionComponent>().Get(request.PlayerId)) != 0)
            {
                ET.Server.MessageHelper.SendActor(sessionId, new A2C_Disconnent());
            }

            StartSceneConfig startSceneConfig = GateAddressHelper.GetGate(scene.DomainZone());

            G2A_LoginGate loginGate =
                    await ActorMessageSenderComponent.Instance.Call(startSceneConfig.InstanceId, new A2G_LoginGate() { PlayerId = request.PlayerId })
                            as G2A_LoginGate;

            response.Gate = startSceneConfig.InnerIPOutPort.ToString();
            response.Token = loginGate.Token;
        }
    }
}