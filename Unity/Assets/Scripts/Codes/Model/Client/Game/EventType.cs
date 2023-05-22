using System.Collections.Generic;

namespace ET.EventType
{
    public struct EnterRoom
    {
        public int RoomId;
    }

    public struct UpdateRoom
    {
    }

    public struct DealCard
    {
        public List<CardInfo> Cards;
    }

    public struct MoCard
    {
        public CardInfo Card;
    }
}