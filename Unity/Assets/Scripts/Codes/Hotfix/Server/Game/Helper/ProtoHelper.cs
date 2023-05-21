namespace ET
{
    [FriendOf(typeof(GameRoom))]
    [FriendOfAttribute(typeof(ET.User))]
    [FriendOfAttribute(typeof(ET.Gamer))]
    public static class ProtoHelper
    {
        public static RoomInfo ToInfo(GameRoom self)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.Players = new();
            roomInfo.Rounds = new();
            roomInfo.RoomId = self.RoomId;
            roomInfo.Status = self.RoomStatus;
            roomInfo.Players = GamerHelper.GamerToGamerInfo(self.DomainScene(), self.Players);

            Log.Info(roomInfo.ToJson() + $"playerCount : {self.Players.Count}");
            return roomInfo;
        }

        
    }
}