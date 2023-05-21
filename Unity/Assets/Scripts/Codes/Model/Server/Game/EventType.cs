namespace ET.Server.EventType
{
    public struct SendPlayerMessage
    {
        public Gamer Player;
        public IActorLocationMessage Message;
    }
}