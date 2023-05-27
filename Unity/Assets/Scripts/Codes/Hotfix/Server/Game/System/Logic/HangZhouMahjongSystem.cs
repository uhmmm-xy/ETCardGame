using System;
using System.Collections.Generic;
using System.Linq;
using ET.Server.EventType;
using MahjongAlgorithm.Algorithm;
using MongoDB.Bson;
using CardEnum = MahjongAlgorithm.Algorithm.Card;

namespace ET
{
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(Card))]
    [FriendOf(typeof(Round))]
    [FriendOf(typeof(HangZhouMahjong))]
    [FriendOfAttribute(typeof(ET.GameRoom))]
    public static class HangZhouMahjongSystem
    {
        public class HangZhouMahjongAwakeSystem : AwakeSystem<HangZhouMahjong>
        {
            protected override void Awake(HangZhouMahjong self)
            {
            }
        }

        public static void DealCard(this HangZhouMahjong self, int gamer)
        {
            Round round = self.GetParent<Round>();
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(gamer);
            List<Card> cards = round.LibCards.GetRange(0, 13);
            cards = cards.SortCard();
            round.LibCards.RemoveRange(0, 13);
            player.HandCards = cards;
            player.Status = PlayerStatus.Waitting;
        }

        public static void RoundStart(this HangZhouMahjong self)
        {
            Round round = self.GetParent<Round>();
            round.Players.ForEach(self.DealCard);
            Card card = round.LibCards.First();
            round.LibCards.Remove(card);
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(round.Players[round.PlayerIndex]);
            self.ChangeGamer(player, round);

            self.CheckPlayerOperate(player, card);

            player.HandCards.Add(card);
            EventSystem.Instance.Publish(self.DomainScene(),
                new SendPlayerMessage() { Player = player, Message = new M2C_MoCard() { Card = card.ToInfo() } });
        }

        public static void NextRound(this HangZhouMahjong self)
        {
            Round round = self.GetParent<Round>();
            round.PlayerIndex++;
            if (round.PlayerIndex >= round.Players.Count)
            {
                round.PlayerIndex = 0;
            }

            Card card = round.LibCards.First();
            round.LibCards.Remove(card);
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(round.Players[round.PlayerIndex]);
            self.ChangeGamer(player, round);
            self.CheckPlayerOperate(player, card);
            player.HandCards.Add(card);
            EventSystem.Instance.Publish(self.DomainScene(),
                new SendPlayerMessage() { Player = player, Message = new M2C_MoCard() { Card = card.ToInfo() } });
        }

        public static void OutCard(this HangZhouMahjong self)
        {
            Round round = self.GetParent<Round>();
            Card outCard = round.OutCards.FirstOrDefault();
            if (outCard is null)
            {
                return;
            }

            foreach (Gamer gamer in round.Players.Select(id => round.DomainScene().GetComponent<GamerComponent>().GetPlayer(id)))
            {
                self.CheckPlayerOperate(gamer, outCard);
            }
        }

        private static void CheckPlayerOperate(this HangZhouMahjong self, Gamer gamer, Card card)
        {
            int operate = OperateType.MahjongNone;

            if (self.CheckPeng(gamer.HandCards.Where(item => !gamer.OpenDeal.Keys.Contains(item, new CardComparer())).ToList(), card) &&
                !self.IsNowPlayer(gamer.PlayerId))
            {
                operate += OperateType.MahjongPeng;
            }

            if (self.CheckGang(gamer.HandCards.Where(item =>
                        !gamer.OpenDeal.Where(i => i.Value is OpenDealType.Gang or OpenDealType.AnGang).Select(x => x.Key)
                                .Contains(item, new CardComparer())).ToList(), card, gamer))
            {
                operate += OperateType.MahjongGang;
            }

            if (self.CheckChi(gamer.HandCards.Where(item => !gamer.OpenDeal.Keys.Contains(item, new CardComparer())).ToList(), card, gamer) &&
                self.IsNextPlayer(gamer.PlayerId))
            {
                operate += OperateType.MahjongChi;
            }

            if (self.CheckHu(gamer.HandCards, card, gamer))
            {
                operate += OperateType.MahjongHu;
            }

            gamer.Operate = operate;

            if (gamer.Operate == OperateType.MahjongNone)
            {
                return;
            }

            self.WaitOperate();
            EventSystem.Instance.Publish(self.DomainScene(),
                new SendPlayerMessage() { Player = gamer, Message = new M2C_OperateCard() { OperateType = operate } });
        }

        public static void WaitOperate(this HangZhouMahjong self)
        {
            Round round = self.GetParent<Round>();
            round.Status = RoundStatus.WaitOperate;
        }

        public static bool IsNowPlayer(this HangZhouMahjong self, int player)
        {
            Round round = self.GetParent<Round>();
            if (player == round.Players[round.PlayerIndex])
            {
                return true;
            }

            return false;
        }

        public static bool IsNextPlayer(this HangZhouMahjong self, int player)
        {
            Round round = self.GetParent<Round>();
            return round.Players.IndexOf(player) == (round.PlayerIndex + 1);
        }

        private static bool CheckPeng(this HangZhouMahjong self, List<Card> handCards, Card card)
        {
            if (card == null)
                return false;
            int isPeng = handCards.Count(item => item.CardValue == card.CardValue && item.CardType == card.CardType);
            return isPeng >= 2;
        }

        private static bool CheckGang(this HangZhouMahjong self, List<Card> handCards, Card card, Gamer gamer)
        {
            //如果是我出牌。我就不可以杠操作
            if (card != null && card.Equals(gamer.OutCards.LastOrDefault()))
            {
                return false;
            }

            var pengAndGang = (from gang in handCards
                               select new { item = gang, count = handCards.Count(c => c.CardValue == gang.CardValue && c.CardType == gang.CardType) }).ToArray();

            if (pengAndGang.Where(item =>
                        !gamer.OpenDeal.Where(i => i.Value == OpenDealType.Peng).Select(i => i.Key).Contains(item.item, new CardComparer()) &&
                        item.count == 3).Select(item => item.item)
                .Contains(card, new CardComparer()))
            {
                return true;
            }

            if (pengAndGang.Any(item =>
                        item.count == 4 && !gamer.OpenDeal.Where(i => i.Value == OpenDealType.Gang).Select(i => i.Key)
                                .Contains(item.item, new CardComparer())))
            {
                return self.IsNowPlayer(gamer.PlayerId);
            }

            return false;
        }

        private static bool CheckChi(this HangZhouMahjong self, List<Card> handCards, Card card, Gamer gamer)
        {
            if (card == null || card.CardType is CardType.Feng or CardType.Jian)
            {
                return false;
            }

            List<Card> chiList = handCards.Where(item =>
                            item.CardType == card.CardType && Math.Abs(item.CardValue - card.CardValue) <= 2 &&
                            Math.Abs(item.CardValue - card.CardValue) > 0 && !gamer.OpenDeal.Select(i => i.Key).Contains(item, new CardComparer()))
                    .Distinct<Card>(new CardComparer()).ToList();

            Log.Info($"Chi Card is ({card.CardType}&{card.CardValue}) ChiList is {chiList.ToJson()} is Count : {chiList.Count}");

            if (chiList.Count <= 1)
            {
                return false;
            }

            Card tem = null;
            bool ret = false;

            foreach (Card item in chiList)
            {
                if (tem == null)
                {
                    tem = item;
                    continue;
                }

                ret = tem.CheckVicinity(item) || ret;
                tem = item;
            }

            return ret;
        }

        private static bool CheckHu(this HangZhouMahjong self, List<Card> handCards, Card card, Gamer gamer)
        {
            List<Card> checkCards = handCards.Where(item => !gamer.OpenDeal.Keys.Contains(item,
                new CardComparer())).ToList();
            // int jokerCount = handCards.Count(item => item is { CardValue: HangZhouMahjong.JokerValue, CardType: HangZhouMahjong.JokerType });
            // gamer.Score = 1;
            // checkCards = checkCards.SortCard();
            // List<Card> jiangList = checkCards
            //         .Where(jiang => (checkCards.Count(item => item.CardValue == jiang.CardValue && item.CardType == jiang.CardValue) >= 2))
            //         .Distinct(new CardComparer()).ToList();
            //
            // foreach (Card jiang in jiangList)
            // {
            //     List<Card> removeJiang = checkCards.RemoveJiang(jiang);
            // }

            IAlgorithm logic = self.DomainScene().GetComponent<GameLogicComponent>().GetLogicHandle() as IAlgorithm;
            List<CardEnum> tingEnum = logic.IsTing(self.CardToLogicCard(checkCards), CardEnum.White).SelectMany(x => x).Distinct().ToList();

            if (tingEnum.Count == 0)
            {
                return false;
            }
            List<Card> ting = self.LogicToCard(tingEnum, gamer);

            GameRoom room = self.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);
            gamer.WinCards.Clear();
            foreach (Card item in ting)
            {
                List<Card> add = room.GetComponent<CardComponent>().GetCard().Where(i => !room.Rounds[room.RoundIndex].OutCards.Contains(i) && gamer.HandCards.Contains(i))
                        .Where(i => i.CardType == item.CardType && i.CardValue == item.CardValue).ToList();
                gamer.WinCards.AddRange(add);
            }
            
            return gamer.WinCards.Contains(card, new CardComparer());
        }

        private static CardEnum[] CardToLogicCard(this HangZhouMahjong self, List<Card> cards)
        {
            CardEnum[] ret = new CardEnum[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                ret[i] = cards[i].ToLogicCard();
            }
            return ret;
        }

        private static List<Card> LogicToCard(this HangZhouMahjong self, List<CardEnum> cards, Gamer gamer)
        {
            GameRoom room = self.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);
            List<Card> ret = new List<Card>();
            foreach (CardEnum card in cards)
            {
                ret.Add(room.GetComponent<CardComponent>().GetCard(card));
            }
            return ret;
        }

        private static CardEnum ToLogicCard(this Card self)
        {
            if (self.CardType is not (CardType.Feng or CardType.Jian))
            {
                return (CardEnum)((self.CardType - 1) * 10 + self.CardValue + 1);
            }
            Dictionary<int, CardEnum> map = new();
            map.Add(50, CardEnum.East);
            map.Add(51, CardEnum.North);
            map.Add(52, CardEnum.South);
            map.Add(53, CardEnum.West);
            map.Add(40, CardEnum.Red);
            map.Add(41, CardEnum.Green);
            map.Add(42, CardEnum.White);
            return map[(self.CardType * 10 + self.CardValue + 1)];
        }

        public static void Operate(this HangZhouMahjong self, Gamer gamer, int operate, List<Card> operateCards)
        {
            switch (operate)
            {
                case OperateType.MahjongChi:
                    self.GamerChiCard(gamer, operateCards);
                    break;
                case OperateType.MahjongPeng:
                    self.GamerPengCard(gamer);
                    break;
                case OperateType.MahjongGang:
                    self.GamerGangCard(gamer, operateCards);
                    break;
                case OperateType.MahjongHu:
                    self.GamerHuCard(gamer);
                    break;
                case OperateType.MahjongNone:
                    self.GamerPass(gamer);
                    break;
            }
        }

        private static void GamerPass(this HangZhouMahjong self, Gamer gamer)
        {
            Round round = self.GetParent<Round>();
            gamer.Operate = OperateType.MahjongNone;
            bool all = round.Players.All(item =>
                    self.DomainScene().GetComponent<GamerComponent>().GetPlayer(item).Operate == OperateType.MahjongNone);
            if (all)
            {
                round.Status = RoundStatus.Next;
            }
        }

        private static void GamerChiCard(this HangZhouMahjong self, Gamer gamer, List<Card> operateCards)
        {
            Round round = self.GetParent<Round>();
            Card last = round.OutCards.First();
            round.OutCards.Remove(last);
            gamer.HandCards.Add(last);
            self.ChangeGamer(gamer, round);

            foreach (Card item in operateCards.Select(
                         card => gamer.HandCards.First(c => c.CardType == card.CardType && c.CardValue == card.CardValue)))
            {
                gamer.OpenDeal.Add(item, OpenDealType.Chi);
            }

            round.Status = RoundStatus.Next;
            self.CheckPlayerOperate(gamer, null);
        }

        private static void GamerPengCard(this HangZhouMahjong self, Gamer gamer)
        {
            Round round = self.GetParent<Round>();
            Card last = round.OutCards.First();
            round.OutCards.Remove(last);
            gamer.HandCards.Add(last);
            self.ChangeGamer(gamer, round);

            List<Card> openList = gamer.HandCards.Where(item => new CardComparer().Equals(item, last)).ToList();
            Log.Info($"Peng List {openList.Count} this : {openList.ToJson()}");
            for (int i = 0; i < 3; i++)
            {
                gamer.OpenDeal.Add(openList[i], OpenDealType.Peng);
            }

            round.Status = RoundStatus.Next;
            self.CheckPlayerOperate(gamer, null);
        }

        private static void GamerGangCard(this HangZhouMahjong self, Gamer gamer, List<Card> operateCards)
        {
            Round round = self.GetParent<Round>();
            int openType = OpenDealType.Gang;
            Card GangCard = round.OutCards.FirstOrDefault();

            if (self.IsNowPlayer(gamer.PlayerId))
            {
                if (!gamer.OpenDeal.Keys.Contains(operateCards?.First(), new CardComparer()))
                {
                    openType = OpenDealType.AnGang;
                }
            }
            else
            {
                round.OutCards.Remove(GangCard);
                gamer.HandCards.Add(GangCard);
            }

            foreach (Card item in operateCards)
            {
                if (gamer.OpenDeal.Keys.Contains(item))
                {
                    gamer.OpenDeal[item] = openType;
                    continue;
                }

                gamer.OpenDeal.Add(item, openType);
            }

            Card outCard = round.LibCards.First();
            round.LibCards.Remove(outCard);
            gamer.OutCards.Add(outCard);
            round.OutCards.Add(outCard);

            Card mo = round.LibCards.First();
            round.LibCards.Remove(mo);
            gamer.HandCards.Add(mo);
            self.ChangeGamer(gamer, round);

            self.Gang = true;
            round.Status = RoundStatus.Next;
            self.CheckPlayerOperate(gamer, mo);
        }

        private static void GamerHuCard(this HangZhouMahjong self, Gamer gamer)
        {
        }

        private static void ChangeGamer(this HangZhouMahjong self, Gamer gamer, Round round)
        {
            self.DomainScene().GetComponent<GamerComponent>().GetPlayer(round.Players[round.PlayerIndex]).Status = PlayerStatus.Waitting;
            round.PlayerIndex = round.Players.IndexOf(gamer.PlayerId);
            gamer.Status = PlayerStatus.Playing;
        }
    }
}