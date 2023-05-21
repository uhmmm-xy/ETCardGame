using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgGameRoom :Entity,IAwake,IUILogic
	{

		public DlgGameRoomViewComponent View { get => this.GetComponent<DlgGameRoomViewComponent>();}

		public int RoomId;

		public List<CardInfo> Cards;
		
		public Dictionary<int, Scroll_Item_Card> ItemCards = new Dictionary<int, Scroll_Item_Card>();

		public CardInfo SelectedCard;
		
	}
}
