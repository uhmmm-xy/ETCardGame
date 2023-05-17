using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgGameRoom))]
    [FriendOf(typeof (Player))]
    public static class DlgGameRoomSystem
    {
        public static void RegisterUIEvent(this DlgGameRoom self)
        {
            self.View.EButton_ExitButton.AddListenerAsync(self.ExitRoom);
        }

        public static void ShowWindow(this DlgGameRoom self, Entity contextData = null)
        {
            //self.View.E_GamePlanImage.SetVisible();
        }

        public static async ETTask InitAsync(this DlgGameRoom self, int roomId)
        {
            RoomInfo info = await GameRoomHelper.GetRoomInfo(self.ClientScene(), roomId);
            Player My = self.DomainScene().GetComponent<Player>();

            List<string> playerids = new();
            int myindex = 0;

            for (int i = 0; i < info.Players.Count; i++)
            {
                string ready = info.Players[i].Status == PlayerStatus.Ready? "Ready" : "";
                playerids.Add($"{My.PlayerId}  {ready}");
                if (info.Players[i].PlayerId == My.PlayerId)
                {
                    myindex = i;
                }
            }

            int index = myindex;
            self.View.ELabel_PlayerDownText.text = playerids[index];
            index = (index + 1) >= playerids.Count? +1 : 0;
            if (playerids.Count > 1) self.View.ELabel_PlayerReightText.text = playerids[index];
            index = (index + 1) >= playerids.Count? +1 : 0;
            if (playerids.Count > 2) self.View.ELabel_PlayerUpText.text = playerids[index];
            index = (index + 1) >= playerids.Count? +1 : 0;
            if (playerids.Count > 3) self.View.ELabel_PlayerLeftText.text = playerids[index];
        }

        public static async ETTask ExitRoom(this DlgGameRoom self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameRoom);
            await ETTask.CompletedTask;
        }
    }
}