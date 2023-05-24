
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_CardDestroySystem : DestroySystem<Scroll_Item_Card> 
	{
		protected override void Destroy( Scroll_Item_Card self )
		{
			self.DestroyWidget();
		}
	}
}
