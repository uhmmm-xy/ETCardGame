namespace ET
{
    [ComponentOf(typeof (GameRoom))]
    public class RoundNextTimer: Entity, IAwake, IDestroy
    {
        public long Timer;
    }
}