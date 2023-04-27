namespace ET
{
    public class Account : Entity,IAwake<string>,IDestroy
    {
        public string OpenId;
        public string CreateTime;
        public int AccountType;
    }

    public enum LoginType
    {
        Wechat,
        Windows,
        Test,
        IOS,
        Android
    }

    public enum AccountType
    {
        BlackList,
        Node,
        Admin
    }
}