using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_AuthTokenHandler: AMRpcHandler<C2G_AuthToken, G2C_AuthToken>
    {
        protected override async ETTask Run(Session session, C2G_AuthToken request, G2C_AuthToken response)
        {
            int playerId = session.DomainScene().GetComponent<TokenComponent>().ExistToken(request.Token);
            if (playerId == 0)
            {
                response.Error = ErrorCode.ERR_AccountIsNull;
                response.Message = "不存在的玩家错误的Token";
                return;
            }

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Account");
            long loginCenterid = startSceneConfig.InstanceId;
            await ActorMessageSenderComponent.Instance.Call(loginCenterid,
                new G2A_GateSession() { PlayerId = playerId, SessionId = session.InstanceId });
            
        }
    }
}