namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        public const int ERR_AccountIsNull = 210001;   //账号为空
        public const int ERR_AccountNotLogin = 210002;  //账号未登录
        public const int ERR_AccountDisconnect = 210003;  //账号断线
        public const int ERR_AccountMuchOpt = 210004;   //账号为空

        public const int ERR_RoomIsNull = 220001; //不存在的房间
        public const int ERR_RoomEnterFail = 220002; //不存在的房间
    }
}