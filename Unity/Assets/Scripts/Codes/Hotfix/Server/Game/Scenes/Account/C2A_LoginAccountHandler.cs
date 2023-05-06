using ET.Server;

namespace ET
{
    [MessageHandler(SceneType.Login)]
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount,A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response)
        {
            await ETTask.CompletedTask;
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error("Is not Scene!!");
                session?.Disconnect();
                return;
            }
            
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (string.IsNullOrEmpty(request.Token))
            {
                response.Error = ErrorCode.ERR_AccountIsNull;
                session?.Disconnect().Coroutine();
                return;
            }
            
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_AccountMuchOpt;
                session?.Disconnect().Coroutine();
                return;
            }
            
            //账号登陆
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.Token.Trim().GetHashCode()))
                {
                    Account account = session.AddChild<Account, string>(request.Token);
                    account = await account.Save();
                    if (!account.IsPlayer())
                    {
                        response.Error = ErrorCode.ERR_AccountNotLogin;
                        session?.Disconnect().Coroutine();
                        return;
                    }

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Account");
                    long loginCenterid = startSceneConfig.InstanceId;
                    L2A_LoginAccountResponse l2ALoginAccountResponse = 
                            await ActorMessageSenderComponent.Instance.Call(loginCenterid, new A2L_LoginAccountRequest() { AccountId = account.Id })
                                    as L2A_LoginAccountResponse;
                    long accountSessionid = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);

                    if (l2ALoginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2ALoginAccountResponse.Error;
                        session?.Disconnect();
                        account?.Dispose();
                        return;
                    }

                    if (accountSessionid != 0)
                    {
                        MessageHelper.SendActor(accountSessionid, new A2C_Disconnect() { Error = 0 });
                    }

                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id, session.InstanceId);
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                    string token = TimeHelper.ServerNow().ToString() + RandomGenerator.RandomNumber(int.MinValue, int.MaxValue);

                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, token);
                    response.Token = token;
                    response.AccountId = account.Id;
                    account?.Dispose();
                }
            }

            
        }
    }
}