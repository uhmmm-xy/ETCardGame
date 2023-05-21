namespace ET.Client
{

    public struct Wait_GameStart: IWaitType
    {
        public int Error { get; set; }
    }
    
    public struct Wait_DealCardEnding: IWaitType
    {
        public int Error { get; set; }
    }
}