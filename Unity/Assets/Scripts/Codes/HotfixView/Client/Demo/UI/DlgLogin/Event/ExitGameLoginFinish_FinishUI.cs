using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class ExitGameLoginFinish_FinishUI : AEvent<ExitGameLoginFinish>
    {
        protected override async ETTask Run(Scene scene, ExitGameLoginFinish a)
        {
            scene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Lobby);
            await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Login);
        }
    }
}