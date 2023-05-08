namespace ET
{
    [ChildOf]
    public class Account: Entity, IAwake<string>, IDestroy
    {
        public int PlayerId;
        public long CreatedTime;
        public int AccountType;
        public long LastLoginTime;
        public int LoginType;
        public int ChannelId;
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
        BlackList = 0,
        Node = 1,
        Admin = 1 << 1,
        AI = 1 << 2
    }
}