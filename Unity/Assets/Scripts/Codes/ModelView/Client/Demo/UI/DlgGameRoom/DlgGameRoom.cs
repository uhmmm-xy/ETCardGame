namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgGameRoom :Entity,IAwake,IUILogic
	{

		public DlgGameRoomViewComponent View { get => this.GetComponent<DlgGameRoomViewComponent>();} 

		 

	}
}
