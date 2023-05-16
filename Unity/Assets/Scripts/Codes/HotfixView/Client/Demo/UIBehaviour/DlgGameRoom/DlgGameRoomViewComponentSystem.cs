
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgGameRoomViewComponentAwakeSystem : AwakeSystem<DlgGameRoomViewComponent> 
	{
		protected override void Awake(DlgGameRoomViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgGameRoomViewComponentDestroySystem : DestroySystem<DlgGameRoomViewComponent> 
	{
		protected override void Destroy(DlgGameRoomViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
