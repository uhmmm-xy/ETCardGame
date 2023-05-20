namespace ET.Game.Scene.Game
{
    [MessageHandler(SceneType.Client)]
    public class G2M_UpdateRoomHandler: AMHandler<G2M_UpdateRoom>
    {
        protected override async ETTask Run(Session session, G2M_UpdateRoom message)
        {
            await EventSystem.Instance.PublishAsync(session.ClientScene(), new EventType.UpdateRoom());
        }
    }
}