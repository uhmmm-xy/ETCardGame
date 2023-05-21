using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class DealCard_ChangeUI : AEvent<DealCard>
    {
        protected override async ETTask Run(Scene scene, DealCard a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().SetCard(a.Cards);
            await ETTask.CompletedTask;
        }
    }
}