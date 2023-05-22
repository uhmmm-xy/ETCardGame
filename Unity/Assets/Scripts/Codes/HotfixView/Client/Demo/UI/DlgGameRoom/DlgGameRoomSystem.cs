using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.EventType;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgGameRoom))]
    [FriendOf(typeof (Player))]
    [FriendOf(typeof (Scroll_Item_Card))]
    public static class DlgGameRoomSystem
    {
        public static void RegisterUIEvent(this DlgGameRoom self)
        {
            self.View.EButton_ExitButton.AddListenerAsync(self.ExitRoom);
            self.View.EButton_ReadyButton.AddListenerAsync(self.ReadyEvent);
            self.View.ELoopScrollList_CardListLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
            {
                self.OnLoopListItemRefreshHandler(transform, index);
            });
            
            self.View.EButton_PengButton.AddListenerAsync(self.SendPeng);
            self.View.EButton_ChiButton.AddListenerAsync(self.SendChi);
            self.View.EButton_GangButton.AddListenerAsync(self.SendGang);
            self.View.EButton_HuButton.AddListenerAsync(self.SendHu);
            self.View.EButton_PassButton.AddListenerAsync(self.SendPass);
            
        }

        public static void ShowWindow(this DlgGameRoom self, Entity contextData = null)
        {
            //self.View.E_GamePlanImage.SetVisible();
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static async ETTask InitAsync(this DlgGameRoom self)
        {
            self.View.ELabel_PlayerLeftText.text = "";
            self.View.ELabel_PlayerReightText.text = "";
            self.View.ELabel_PlayerUpText.text = "";
            self.View.ELabel_PlayerDownText.text = "";
            self.PlayerOutText.Clear();

            RoomInfo info = await GameRoomHelper.GetRoomInfo(self.ClientScene(), self.RoomId);
            Player My = self.DomainScene().GetComponent<Player>();

            List<string> playerids = new();
            int myindex = 0;

            for (int i = 0; i < info.Players.Count; i++)
            {
                string ready = info.Players[i].Status == PlayerStatus.Ready? "Ready" : "";
                playerids.Add($"{info.Players[i].PlayerId}  {ready}");
                if (info.Players[i].PlayerId == My.PlayerId)
                {
                    myindex = i;
                }
            }

            int index = myindex;
            self.View.ELabel_PlayerDownText.text = playerids[index];
            self.PlayerOutText.Add(info.Players[index].PlayerId, self.View.ELabel_DownOutText);
            index = self.NextPlayer(index, playerids.Count);
            if (playerids.Count > 1)
            {
                self.PlayerOutText.Add(info.Players[index].PlayerId, self.View.ELabel_ReightOutText);
                self.View.ELabel_PlayerReightText.text = playerids[index];
            }

            index = self.NextPlayer(index, playerids.Count);
            if (playerids.Count > 2)
            {
                self.PlayerOutText.Add(info.Players[index].PlayerId, self.View.ELabel_UpOutText);
                self.View.ELabel_PlayerUpText.text = playerids[index];
            }

            index = self.NextPlayer(index, playerids.Count);
            if (playerids.Count > 3)
            {
                self.PlayerOutText.Add(info.Players[index].PlayerId, self.View.ELabel_LeftOutText);
                self.View.ELabel_PlayerLeftText.text = playerids[index];
            }
        }

        static int NextPlayer(this DlgGameRoom self, int index, int count)
        {
            index++;
            if (index >= count)
            {
                index = 0;
            }

            Log.Info($"index change {index}");
            return index;
        }

        public static async ETTask ExitRoom(this DlgGameRoom self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GameRoom);
            await ETTask.CompletedTask;
        }

        public static async ETTask ReadyEvent(this DlgGameRoom self)
        {
            await GameRoomHelper.GamerReady(self.ClientScene());
        }

        public static void SetOutCards(this DlgGameRoom self, List<OutCardMap> map)
        {
            foreach (OutCardMap item in map)
            {
                if (item.Outs == null) continue;
                string outstr = item.Outs.Aggregate("",
                    (current, card) => current + $"{card.ToEnity().GetValueName()} {card.ToEnity().GetTypeName()} |");
                self.PlayerOutText[item.PlayerId].text = outstr;
            }
        }

        public static void SetCard(this DlgGameRoom self, List<CardInfo> cards)
        {
            self.RemoveUIScrollItems(ref self.ItemCards);
            self.SelectedCard = null;
            self.SelectIndex = -1;
            self.Cards = cards;
            self.AddUIScrollItems(ref self.ItemCards, cards.Count);
            self.View.ELoopScrollList_CardListLoopHorizontalScrollRect.SetVisible(true, cards.Count);
        }

        public static void OnLoopListItemRefreshHandler(this DlgGameRoom self, Transform transform, int index)
        {
            Scroll_Item_Card itemCard = self.ItemCards[index].BindTrans(transform);
            itemCard.EButton_CardImage.color = index == self.SelectIndex? Color.green : Color.gray;
            itemCard.Card = self.Cards[index];
            itemCard.ELabel_CardValueText.text =
                    self.Cards[index].ToEnity().GetValueName() + "\r\n" + self.Cards[index].ToEnity().GetTypeName();
            itemCard.EButton_CardButton.AddListenerAsync<int>(self.SelectCardEvent, index);
        }

        public static async ETTask SelectCardEvent(this DlgGameRoom self, int index)
        {
            Scroll_Item_Card card = self.ItemCards[index];
            if (card.Card == self.SelectedCard)
            {
                self.SetCard(await GameRoomHelper.OutCard(self.ClientScene(), card.Card));
                Log.Info("出牌" + card.Card.ToEnity().GetTypeName() + "---" + card.Card.ToEnity().GetValueName());
                return;
            }

            if (self.SelectIndex != -1)
            {
                self.ItemCards[self.SelectIndex].EButton_CardImage.color = Color.gray;
            }

            self.SelectedCard = card.Card;
            self.SelectIndex = index;

            card.EButton_CardImage.color = Color.green;
            await ETTask.CompletedTask;
        }

        public static void AddCard(this DlgGameRoom self, CardInfo card)
        {
            self.RemoveUIScrollItems(ref self.ItemCards);
            self.Cards.Add(card);
            self.SelectedCard = card;
            self.SelectIndex = self.Cards.IndexOf(card);
            self.AddUIScrollItems(ref self.ItemCards, self.Cards.Count);
            self.View.ELoopScrollList_CardListLoopHorizontalScrollRect.SetVisible(true, self.Cards.Count);
        }

        public static async ETTask SendPeng(this DlgGameRoom self)
        {
            await ETTask.CompletedTask;
        }
        
        public static async ETTask SendChi(this DlgGameRoom self)
        {
            await ETTask.CompletedTask;
        }
        
        public static async ETTask SendGang(this DlgGameRoom self)
        {
            await ETTask.CompletedTask;
        }
        
        public static async ETTask SendHu(this DlgGameRoom self)
        {
            await ETTask.CompletedTask;
        }
        
        public static async ETTask SendPass(this DlgGameRoom self)
        {
            await ETTask.CompletedTask;
        }
    }
}