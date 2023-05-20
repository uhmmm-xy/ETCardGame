using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    [FriendOf(typeof(DlgGameRoom))]
    public class EnterRoom_FinshEvent : AEvent<EnterRoom>
    {
        protected override async ETTask Run(Scene scene, EnterRoom enterRoom)
        {

            await scene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_GameRoom);

            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.ELabel_RoomIdText.text = enterRoom.RoomId.ToString();
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().RoomId = enterRoom.RoomId;

            await scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().InitAsync();

        }
    }
}