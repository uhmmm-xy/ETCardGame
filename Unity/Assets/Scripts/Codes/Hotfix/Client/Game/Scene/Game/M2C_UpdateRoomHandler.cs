namespace ET.Game.Scene.Game
{
    [MessageHandler(SceneType.Client)]
    public class M2C_UpdateRoomHandler: AMHandler<M2C_UpdateRoom>
    {
        protected override async ETTask Run(Session session, M2C_UpdateRoom message)
        {
            await EventSystem.Instance.PublishAsync(session.ClientScene(), new EventType.UpdateRoom());
        }
    }
}