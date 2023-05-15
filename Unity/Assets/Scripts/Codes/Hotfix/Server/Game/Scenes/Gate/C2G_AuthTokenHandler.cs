using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.Gate)]
    [FriendOf(typeof (Account))]
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


            Account account = await AccountHelper.GetAccount(playerId);
            if (session.DomainScene().GetComponent<PlayerComponent>().GetChild<Account>(account.Id)!=null)
            {
                session.DomainScene().GetComponent<PlayerComponent>().RemoveChild(account.Id);
            }
            session.DomainScene().GetComponent<PlayerComponent>().AddChild(account);
            await account.Init();
            
            account.AddComponent<SessionInfoComponent>().Session = session;

            session.AddComponent(account);

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Account");
            long loginCenterid = startSceneConfig.InstanceId;
            ActorMessageSenderComponent.Instance.Send(loginCenterid,
                new G2A_GateSession() { PlayerId = account.PlayerId, SessionId = session.InstanceId });

            account.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
            await account.AddLocation(LocationType.Player);

            response.Info = account.GetComponent<User>().ToInfo();
        }
    }
}