using ET.Server;

namespace ET
{
    [ActorMessageHandler(SceneType.Gate)]
    public class G2A_LoginGateHandler : AMActorRpcHandler<Scene,A2G_LoginGate,G2A_LoginGate>
    {
        protected override async ETTask Run(Scene scene, A2G_LoginGate request, G2A_LoginGate response)
        {
            string token = MD5Helper.FileMD5((request.PlayerId + TimeHelper.ServerNow()).ToString());
            scene.GetComponent<TokenComponent>().Add(request.PlayerId,token);
            response.Token = token;
            await ETTask.CompletedTask;
        }
    }
}