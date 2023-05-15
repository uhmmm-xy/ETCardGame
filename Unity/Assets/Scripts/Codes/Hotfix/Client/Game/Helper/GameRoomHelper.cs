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

        public static async ETTask EnterRoom(Scene client, int RoomId)
        {
            await EventSystem.Instance.PublishAsync(client, new EventType.EnterRoom() { RoomId = RoomId });
        }
    }
}