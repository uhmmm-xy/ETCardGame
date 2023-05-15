using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgLobby))]
    [FriendOf(typeof(Player))]
    public static class DlgLobbySystem
    {

        public static void RegisterUIEvent(this DlgLobby self)
        {
            self.View.E_EnterGameButton.AddListenerAsync(self.CreateRoom);
            self.View.E_ExitGameButton.AddListenerAsync(self.ExitGame);
            self.View.EButton_closeButton.AddListenerAsync(self.ClosePlan);
            self.View.EButton_PushRoomButton.AddListenerAsync(self.PushRoom);
        }

        public static void ShowWindow(this DlgLobby self, Entity contextData = null)
        {
            self.View.E_PushGamePlanImage.SetVisible(false);
            Player player = self.DomainScene().GetComponent<Player>();
            self.View.ELabel_PlayerIdText.text = $"玩家名：{player.PlayerId}";
            self.View.ELabel_GlodText.text = $"房卡数：{player.Jewel}";
        }

        public static async ETTask ExitGame(this DlgLobby self)
        {
            Log.Info("退出游戏！！！！");
            await LoginHelper.ExitGame(self.ClientScene());
        }

        public static async ETTask CreateRoom(this DlgLobby self)
        {
            self.View.E_PushGamePlanImage.SetVisible(true);
            await GameRoomHelper.CreatedRoom(self.ClientScene());
            //await EnterMapHelper.EnterMapAsync(self.ClientScene());
            //self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Lobby);
        }

        public static async ETTask PushRoom(this DlgLobby self)
        {
            int roomId = int.Parse(self.View.E_RoomIdInputField.text);
            await GameRoomHelper.EnterRoom(self.ClientScene(),roomId);
        }

        public static async ETTask ClosePlan(this DlgLobby self)
        {
            self.View.E_RoomIdInputField.text = "";
            self.View.E_PushGamePlanImage.SetVisible(false);
            await ETTask.CompletedTask;
        }

    }
}
