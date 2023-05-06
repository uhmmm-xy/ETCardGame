using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Account)]
    public class L2A_LoginAccountHandler : AMActorRpcHandler<Scene,L2A_LoginAccount,A2L_LoginAccount>
    {
        protected override async ETTask Run(Scene scene, L2A_LoginAccount request, A2L_LoginAccount response)
        {
            await ETTask.CompletedTask;
            //string token = scene.GetComponent<AccountComponent>().Add(request.PlayerId);
        }
    }
}