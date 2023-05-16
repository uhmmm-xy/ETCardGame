using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgGameRoom))]
	public static  class DlgGameRoomSystem
	{

		public static void RegisterUIEvent(this DlgGameRoom self)
		{
			self.View.EButton_ExitButton.AddListenerAsync(self.ExitRoom);
		}

		public static void ShowWindow(this DlgGameRoom self, Entity contextData = null)
		{
		}

		public static async ETTask ExitRoom(this DlgGameRoom self)
		{
			
			self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameRoom);
			await ETTask.CompletedTask;
		}

		 

	}
}
