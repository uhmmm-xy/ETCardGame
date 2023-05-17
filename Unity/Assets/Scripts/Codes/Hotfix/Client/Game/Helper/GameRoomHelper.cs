namespace ET.Client
{
    public static class GameRoomHelper
    {
        public static async ETTask CreatedRoom(Scene client)
        {
            G2C_CreatedRoom g2CCreatedRoom =
                    await client.GetComponent<SessionComponent>().Session.Call(new C2G_CreatedRoom() { GameId = 1001 }) as G2C_CreatedRoom;

            if (g2CCreatedRoom.Error == ErrorCode.ERR_Success)
            {
                await EventSystem.Instance.PublishAsync(client, new EventType.EnterRoom() { RoomId = g2CCreatedRoom.RoomId });
            }
        }

        public static async ETTask EnterRoom(Scene client, int roomId)
        {
            G2C_EnterRoom g2CEnterRoom =
                    await client.GetComponent<SessionComponent>().Session.Call(new C2G_EnterRoom() { GameId = 1001, RoomId = roomId }) as
                            G2C_EnterRoom;
            if (g2CEnterRoom.Error != ErrorCode.ERR_Success)
            {
                Log.Info($"ErrorCode: {g2CEnterRoom.Error} Message : {g2CEnterRoom.Message}");
                return;
            }

            await EventSystem.Instance.PublishAsync(client, new EventType.EnterRoom() { RoomId = roomId });
        }

        public static async ETTask<RoomInfo> GetRoomInfo(Scene client, int roomId)
        {
            M2C_RoomInfo m2CRoomInfo =
                    await client.GetComponent<SessionComponent>().Session.Call(new C2M_RoomInfo() { RoomId = roomId }) as M2C_RoomInfo;
            return m2CRoomInfo.Info;
        }
    }
}