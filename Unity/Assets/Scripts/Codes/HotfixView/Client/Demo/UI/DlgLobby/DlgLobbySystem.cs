using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgLobby))]
	public static  class DlgLobbySystem
	{

		public static void RegisterUIEvent(this DlgLobby self)
		{
			self.View.E_EnterGameButton.AddListenerAsync(self.EnterMap);
			self.View.E_ExitGameButton.AddListenerAsync(self.ExitGame);
		}

		public static void ShowWindow(this DlgLobby self, Entity contextData = null)
		{
		}

		public static async ETTask ExitGame(this DlgLobby  self)
		{
			Log.Info("退出游戏！！！！");
			await ETTask.CompletedTask;
		}

		public static async ETTask EnterMap(this DlgLobby self)
		{
			await EnterMapHelper.EnterMapAsync(self.ClientScene());
			self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Lobby);
		}

	}
}
