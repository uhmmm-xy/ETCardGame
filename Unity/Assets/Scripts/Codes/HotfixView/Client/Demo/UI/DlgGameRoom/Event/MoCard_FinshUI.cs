using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class MoCard_FinshUI : AEvent<MoCard>
    {
        protected override async ETTask Run(Scene scene, MoCard a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().AddCard(a.Card);
            await ETTask.CompletedTask;
        }
    }
}