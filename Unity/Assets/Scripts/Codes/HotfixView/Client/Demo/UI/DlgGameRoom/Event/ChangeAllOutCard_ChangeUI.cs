using ET.EventType;

namespace ET.Client
{
    
    [Event(SceneType.Client)]
    public class ChangeAllOutCard_ChangeUI : AEvent<ChangeAllOutCard>
    {
        protected override async ETTask Run(Scene scene, ChangeAllOutCard a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().SetOutCards(a.OutCardMaps);
            await ETTask.CompletedTask;
        }
    }
}