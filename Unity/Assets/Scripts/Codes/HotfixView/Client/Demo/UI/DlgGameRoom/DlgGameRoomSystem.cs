using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.EventType;
using MongoDB.Bson;
using Unity.Mathematics;
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
            self.OpenCards = info.Players[index].OpenDeal;
            if (info.Players[index].OpenDeal != null)
            {
                List<CardInfo> RemoveOpenList = info.Players[index].HandCards.ToList();
                foreach (CardInfo ca in info.Players[index].OpenDeal.Select(item => item.Card))
                {
                    RemoveOpenList.Remove(RemoveOpenList.First(item => CardInfoHelper.Equals(item, ca)));
                }

                Log.Info($"Remove out {RemoveOpenList.Count} this {RemoveOpenList.ToJson()}");
                self.SetCard(RemoveOpenList);
            }
            else
            {
                self.SetCard(info.Players[index].HandCards);
            }

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
            GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongPeng, new List<CardInfo>());
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void SendChi(this DlgGameRoom self)
        {
            RoundInfo round = self.SeflInfo.Rounds.Last();
            CardInfo lastCard = round.OutCard.First();
            List<CardInfo> Chilist = self.Cards.Where(item =>
                    item.Type == lastCard.Type && math.abs(item.Value - lastCard.Value) < 2 && math.abs(item.Value - lastCard.Value) > 0).ToList();
            Chilist = Chilist.Select(item => item.ToEnity()).Distinct(new CardComparer()).Select(item => item.ToInfo()).ToList();

            if (Chilist.Count > 2)
            {
                self.ShowChiOperateBtn(Chilist, lastCard);
                return;
            }

            GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongGang, Chilist);
            self.View.E_OpatePlanImage.SetVisible(false);
        }

        public static void SendGang(this DlgGameRoom self)
        {
            RoundInfo round = self.SeflInfo.Rounds.LastOrDefault();
            CardInfo lastCard = round.OutCard?.FirstOrDefault();

            List<CardInfo> gang = self.Cards.Where(item => CardInfoHelper.Equals(item, lastCard)).ToList();
            if (gang.Count == 3)
            {
                GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongGang, gang);
                return;
            }

            var joinList = self.OpenCards?.Where(item => item.OpenType == OpenDealType.Peng).Select(item => item.Card).ToList();

            gang = (from map in (from cards in self.Cards.Concat(joinList ?? new List<CardInfo>())
                    select new
                    {
                        card = cards,
                        count = self.Cards.Concat(joinList ?? new List<CardInfo>())
                                .Count(item => CardInfoHelper.Equals(item, cards))
                    })
                where map.count == 4
                select map.card).ToList();

            Log.Info($"Gang count:{gang.Count}");

            if (gang.Count > 4)
            {
                self.View.E_SelectCardsImage.SetVisible(true);
                List<Card> selItem = gang.Select(item => item.ToEnity()).Distinct(new CardComparer()).ToList();
                for (int i = 0; i < selItem.Count; i++)
                {
                    self.GetSelectIndex(i).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                        new SelectCardMap() { Card = selItem[i].ToInfo(), Type = SelectCardType.MahjongGang });
                    self.GetSelectPlanIndex(i).SetVisible(true);
                }

                return;
            }

            if (gang.Count < 4)
            {
                Log.Error($"找不到要杠的数组。 {gang.Count} {self.Cards.Concat(joinList ?? new List<CardInfo>()).ToJson()},Join : {joinList.ToJson()} , map:{self.OpenCards.ToJson()}");
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
            foreach (Scroll_Item_Card item in self.ItemCards.Values)
            {
                if (item.uiTransform != null)
                {
                    item.EButton_CardImage.color = Color.gray;
                }
            }

            List<Scroll_Item_Card> selCards = null;
            List<CardInfo> selInfo = null;
            switch (card.Type)
            {
                case SelectCardType.MahjongGang:
                    selCards = self.ItemCards.Select(a => a.Value).Where(a => CardInfoHelper.Equals(a.Card, card.Card)).ToList();
                    selInfo = self.Cards
                            .Concat(self.OpenCards?.Where(item => item.OpenType == OpenDealType.Peng).Select(item => item.Card) ??
                                new List<CardInfo>()).Where(item => CardInfoHelper.Equals(item, card.Card))
                            .ToList();
                    break;
                case SelectCardType.MahjongChiLeft:
                    selInfo = self.Cards.Where(item =>
                            item.Type == card.Card.Type && (item.Value - card.Card.Value) < 2 && (item.Value - card.Card.Value) > 0).ToList();
                    selInfo = selInfo.Select(item => item.ToEnity()).Distinct(new CardComparer()).Select(item => item.ToInfo()).ToList();
                    selCards = self.GetChiCardBtn(selInfo);
                    break;
                case SelectCardType.MahjongChiMiddle:
                    selInfo = self.Cards.Where(item => item.Type == card.Card.Type && Math.Abs(item.Value - card.Card.Value) == 1).ToList();
                    selInfo = selInfo.Select(item => item.ToEnity()).Distinct(new CardComparer()).Select(item => item.ToInfo()).ToList();
                    selCards = self.GetChiCardBtn(selInfo);
                    break;
                case SelectCardType.MahjongChiReight:
                    selInfo = self.Cards.Where(item =>
                            item.Type == card.Card.Type && (item.Value - card.Card.Value) > -2 && (item.Value - card.Card.Value) < 0).ToList();
                    selInfo = selInfo.Select(item => item.ToEnity()).Distinct(new CardComparer()).Select(item => item.ToInfo()).ToList();
                    selCards = self.GetChiCardBtn(selInfo);
                    break;
            }

            if (card.Equals(self.SelectedCardMap))
            {
                Log.Info($"select CardMap is {card.Type} {card.Card.Type} {card.Card.Value}");
                GameRoomHelper.GamerOperate(self.ClientScene(), OperateType.MahjongGang, selInfo);
                self.View.E_SelectCardsImage.SetVisible(false);
                return;
            }

            self.SelectedCardMap = card;

            foreach (Scroll_Item_Card item in selCards)
            {
                if (item.uiTransform != null)
                {
                    item.EButton_CardImage.color = Color.red;
                }
            }
        }

        public static List<Scroll_Item_Card> GetChiCardBtn(this DlgGameRoom self, List<CardInfo> infos)
        {
            List<Scroll_Item_Card> ret = new();
            Scroll_Item_Card tem = null;
            foreach (Scroll_Item_Card item in self.ItemCards.Values.Where(a => infos.Any(i => i.Value == a.Card.Value && i.Type == a.Card.Type)))
            {
                if (tem == null)
                {
                    tem = item;
                    continue;
                }

                if (tem != item)
                {
                    ret.Add(tem);
                    ret.Add(item);
                    break;
                }

                tem = item;
            }

            return ret;
        }

        public static void ShowChiOperateBtn(this DlgGameRoom self, List<CardInfo> infos, CardInfo source)
        {
            if (infos.Count == 4)
            {
                self.GetSelectIndex(0).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                    new SelectCardMap() { Card = source, Type = SelectCardType.MahjongChiLeft });
                self.GetSelectPlanIndex(0).SetVisible(true);
                self.GetSelectIndex(1).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                    new SelectCardMap() { Card = source, Type = SelectCardType.MahjongChiMiddle });
                self.GetSelectPlanIndex(1).SetVisible(true);
                self.GetSelectIndex(2).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                    new SelectCardMap() { Card = source, Type = SelectCardType.MahjongChiReight });
                self.GetSelectPlanIndex(2).SetVisible(true);
                return;
            }

            bool isLeft = (source.Value - infos.First().Value == 2);

            self.GetSelectIndex(1).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                new SelectCardMap() { Card = source, Type = SelectCardType.MahjongChiMiddle });
            self.GetSelectPlanIndex(1).SetVisible(true);

            if (isLeft)
            {
                self.GetSelectIndex(0).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                    new SelectCardMap() { Card = source, Type = SelectCardType.MahjongChiLeft });
                self.GetSelectPlanIndex(0).SetVisible(true);
                return;
            }

            self.GetSelectIndex(2).AddListenerWithParam<SelectCardMap>(self.OperateSelect,
                new SelectCardMap() { Card = source, Type = SelectCardType.MahjongChiReight });
            self.GetSelectPlanIndex(2).SetVisible(true);
        }
    }
}