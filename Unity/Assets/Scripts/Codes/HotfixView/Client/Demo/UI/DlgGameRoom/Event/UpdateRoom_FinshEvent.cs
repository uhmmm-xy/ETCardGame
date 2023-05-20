using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class UpdateRoom_FinshEvent : AEvent<UpdateRoom>
    {
        protected override async ETTask Run(Scene scene, UpdateRoom updateRoom)
        {
            await scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().InitAsync();
        }
    }
}