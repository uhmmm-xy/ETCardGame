using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(GameRoom))]
    [FriendOfAttribute(typeof(ET.User))]
    [FriendOfAttribute(typeof(ET.Gamer))]
    public static class ProtoHelper
    {
        public static RoomInfo ToInfo(GameRoom self,int myId)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.Rounds = ProtoHelper.RoundToInfo(self.Rounds);
            roomInfo.RoomId = self.RoomId;
            roomInfo.Status = self.RoomStatus;
            roomInfo.Players = GamerHelper.GamerToGamerInfo(self.DomainScene(), self.Players,myId);

            Log.Info(roomInfo.ToJson() + $"playerCount : {self.Players.Count}");
            return roomInfo;
        }

        public static List<RoundInfo> RoundToInfo(List<Round> rounds)
        {
            List<RoundInfo> list = new();
            foreach (Round item in rounds)
            {
                list.Add(item.ToInfo());
            }

            return list;
        } 


    }
}