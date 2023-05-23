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

            self.View.EButton_PengButton.AddListener(self.SendPeng);
            self.View.EButton_ChiButton.AddListener(self.SendChi);
            self.View.EButton_GangButton.AddListener(self.SendGang);
            self.View.EButton_HuButton.AddListener(self.SendHu);
            self.View.EButton_PassButton.AddListener(self.SendPass);
        }

        public static void ShowWindow(this DlgGameRoom self, Entity contextData = null)
        {
            //self.View.E_GamePlanImage.SetVisible();
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static Image GetSelectPlanIndex(this DlgGameRoom self, int index)
        {
            switch (index)
            {
                case 0:
                    return self.View.EButton_SelectOneImage;
                case 1:
                    return self.View.EButton_SelectTwoImage;
                case 2:
                    return self.View.EButton_SelectThreeImage;
                default:
                    return self.View.EButton_SelectOneImage;
            }
        }

        public static Button GetSelectIndex(this DlgGameRoom self, int index)
        {
            switch (index)
            {
                case 0:
                    return self.View.EButton_SelectOneButton;
                case 1:
                    return self.View.EButton_SelectTwoButton;
                case 2:
                    return self.View.EButton_SelectThreeButton;
                default:
                    return self.View.EButton_SelectOneButton;
            }
        }

        public static async ETTask InitAsync(this DlgGameRoom self)
        {
            self.View.ELabel_LeftPlayerText.text = "";
            self.View.ELabel_PlayerReightText.text = "";
            self.View.ELabel_PlayerUpText.text = "";
            self.View.ELabel_PlayerDownText.text = "";
            self.View.ELabel_PlayerDownText.color = Color.black;
            self.View.ELabel_PlayerUpText.color = Color.black;
            self.View.ELabel_PlayerReightText.color = Color.black;
            self.View.ELabel_LeftPlayerText.color = Color.black;

            RoomInfo info = await GameRoomHelper.GetRoomInfo(self.ClientScene(), self.RoomId);

            self.SeflInfo = info;

            if (info.Status == RoomStatus.Gameing)
            {
                self.View.E_BeginBtnPlanImage.SetVisible(false);
            }

            Player My = self.DomainScene().GetComponent<Player>();

            List<string> playerids = new();
            int myindex = 0;

            for (int i = 0; i < info.Players.Count; i++)
            {
                string ready = info.Players[i].Status == PlayerStatus.None? "" : "Ready";
                playerids.Add($"{info.Players[i].PlayerId}  {ready}");
                if (info.Players[i].PlayerId == My.PlayerId)
                {
                    myindex = i;
                }
            }

            int index = myindex;
            self.View.ELabel_PlayerDownText.text = playerids[index];
            self.SetCardText(info.Players[index].OutCards, self.View.ELabel_DownOutText);
            self.SetCardText(info.Players[index].OpenDeal, self.View.E_DownOpenDealText);
            self.SetCard(info.Players[index].HandCards);

            if (info.Players[index].Status == PlayerStatus.Playing)
            {
                self.Operate = true;
                self.View.ELabel_PlayerDownText.color = Color.red;
            }

            index = self.NextPlayer(index, playerids.Count);
            if (playerids.Count > 1)
            {
                self.View.ELabel_PlayerReightText.text = playerids[index];
                self.SetCardText(info.Players[index].OutCards, self.View.ELabel_ReightOutText);
                self.SetCardText(info.Players[index].OpenDeal, self.View.E_ReightOpenDealText);
                if (info.Players[index].Status == PlayerStatus.Playing)
                {
                    self.View.ELabel_PlayerReightText.color = Color.red;
                }
            }

            index = self.NextPlayer(index, playerids.Count);
            if (playerids.Count > 2)
            {
                self.View.ELabel_PlayerUpText.text = playerids[index];
                self.SetCardText(info.Players[index].OutCards, self.View.ELabel_UpOutText);
                self.SetCardText(info.Players[index].OpenDeal, self.View.E_UpOpenDealText);
                if (info.Players[index].Status == PlayerStatus.Playing)
                {
                    self.View.ELabel_PlayerUpText.color = Color.red;
                }
            }

            index = self.NextPlayer(index, playerids.Count);
            if (playerids.Count > 3)
            {
                self.View.ELabel_LeftPlayerText.text = playerids[index];
                self.SetCardText(info.Players[index].OutCards, self.View.ELabel_LeftOutText);
                self.SetCardText(info.Players[index].OpenDeal, self.View.E_LeftOpenDealText);
                if (info.Players[index].Status == PlayerStatus.Playing)
                {
                    self.View.ELabel_LeftPlayerText.color = Color.red;
                }
            }
        }

        private static void SetCardText(this DlgGameRoom self, List<CardInfo> cards, Text text)
        {
            if (cards == null) return;
            text.text = cards.Aggregate("",
                (current, card) => current + $"{card.ToEnity().GetValueName()} {card.ToEnity().GetTypeName()} ||");
        }

        private static void SetCardText(this DlgGameRoom self, List<OpenDealMap> cards, Text text)
        {
            if (cards == null) return;
            text.text = cards.Aggregate("",
                (current, card) => current + $"{card.Card.ToEnity().GetValueName()} {card.Card.ToEnity().GetTypeName()} ||");
        }

        private static int NextPlayer(this DlgGameRoom self, int index, int count)
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

        public static void SetCard(this DlgGameRoom self, List<CardInfo> cards)
        {
            if (cards == null) return;
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
                    self.Cards[index].ToEnity().GetValueName() + "\r\n\r\n" + self.Cards[index].ToEnity().GetTypeName();
            itemCard.EButton_CardButton.AddListenerWithId(self.SelectCardEvent, index);
        }

        public static void SelectCardEvent(this DlgGameRoom self, int index)
        {
            Log.Info($"Select Card Index {index}");
            // Scroll_Item_Card card = self.ItemCards[index];
            CardInfo card = self.Cards[index];
            if (card == self.SelectedCard)
            {
                if (!self.Operate)
                {
                    return;
                }

                GameRoomHelper.OutCard(self.ClientScene(), card);
                Log.Info("出牌" + card.ToEnity().GetTypeName() + "---" + card.ToEnity().GetValueName());
                self.Operate = false;
                return;
            }

            self.SelectedCard = card;
            self.SelectIndex = index;
        }

        public static void AddCard(this DlgGameRoom self, CardInfo card)
        {
            self.Operate = true;
            self.RemoveUIScrollItems(ref self.ItemCards);
            self.Cards.Add(card);
            self.SelectedCard = card;
            self.SelectIndex = self.Cards.IndexOf(card);
            self.AddUIScrollItems(ref self.ItemCards, self.Cards.Count);
            self.View.ELoopScrollList_CardListLoopHorizontalScrollRect.SetVisible(true, self.Cards.Count);
        }

        public static void SendPeng(this DlgGameRoom self)
        {
            GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongChi, new List<CardInfo>());
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void SendChi(this DlgGameRoom self)
        {
            //self.SeflInfo;
            RoundInfo round = self.SeflInfo.Rounds.Last();
            CardInfo lastCard = round.OutCard.First();
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void SendGang(this DlgGameRoom self)
        {
            RoundInfo round = self.SeflInfo.Rounds.Last();
            CardInfo lastCard = round.OutCard.First();

            List<CardInfo> gang = self.Cards.Where(item => item.Value == lastCard.Value && lastCard.Type == item.Type).ToList();
            if (gang.Count == 4)
            {
                GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongGang, gang);
                return;
            }

            gang = (from map in (from cards in self.Cards
                    select new { card = cards, count = self.Cards.Count(item => item.Value == cards.Value && item.Type == cards.Type) })
                where map.count == 4
                select map.card).ToList();
            if (gang.Count > 4)
            {
                List<Card> selItem = gang.Select(item => item.ToEnity()).Distinct(new CardComparer()).ToList();
                for (int i = 0; i < selItem.Count; i++)
                {
                    self.GetSelectIndex(i).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                        new SelectCardMap() { Card = selItem[i].ToInfo(), Type = SelectCardType.MahjongGang });
                    self.GetSelectPlanIndex(i).SetVisible(true);
                }

                return;
            }

            GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongGang, gang);
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void SendHu(this DlgGameRoom self)
        {
            GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongHu, new List<CardInfo>());
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void SendPass(this DlgGameRoom self)
        {
            GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongNone, new List<CardInfo>());
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void OperateSelect(this DlgGameRoom self, SelectCardMap card)
        {
            List<Scroll_Item_Card> selCards = null;
            switch (card.Type)
            {
                case SelectCardType.MahjongGang:
                    selCards = self.ItemCards.Select(a => a.Value).Where(a => a.Card == card.Card).ToList();
                    break;
                case SelectCardType.MahjongChiLeft:
                    break;
                case SelectCardType.MahjongChiMiddle:
                    break;
                case SelectCardType.MahjongChiReight:
                    break;
            }

            foreach (Scroll_Item_Card item in selCards)
            {
                item.EButton_CardButton.Select();
            }
        }
    }
}