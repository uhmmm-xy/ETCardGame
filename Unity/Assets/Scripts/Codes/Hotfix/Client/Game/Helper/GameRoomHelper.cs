using System.Collections.Generic;

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

        public static async ETTask GamerReady(Scene client)
        {
            M2C_GamerReady m2CGamerReady = await client.GetComponent<SessionComponent>().Session.Call(new C2M_GamerReady()) as M2C_GamerReady;
            if (m2CGamerReady.Error != ErrorCode.ERR_Success)
            {
                Log.Info($"ErrorCode: {m2CGamerReady.Error} Message : {m2CGamerReady.Message}");
                return;
            }

            await EventSystem.Instance.PublishAsync(client, new EventType.UpdateRoom());
            client.GetComponent<ObjectWait>().Notify(new Wait_GameStart());
        }

        public static async ETTask<RoomInfo> GetRoomInfo(Scene client, int roomId)
        {
            M2C_RoomInfo m2CRoomInfo =
                    await client.GetComponent<SessionComponent>().Session.Call(new C2M_RoomInfo() { RoomId = roomId }) as M2C_RoomInfo;
            return m2CRoomInfo.Info;
        }

        public static void OutCard(Scene client, CardInfo card)
        {
            client.GetComponent<SessionComponent>().Session.Send(new C2M_OutCard() { Card = card });
        }

        public static void GamerOperate(Scene client, int operate, List<CardInfo> operateCards)
        {
            client.GetComponent<SessionComponent>().Session.Send(new C2M_GamerOperate() { Operate = operate, OperateCards = operateCards });
        }
    }
}