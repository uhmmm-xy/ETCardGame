using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using ET.Server.EventType;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (Gamer))]
    [FriendOf(typeof (Card))]
    [FriendOf(typeof (Round))]
    [FriendOf(typeof (HangZhouMahjong))]
    public static class HangZhouMahjongSystem
    {
        public class HangZhouMahjongAwakeSystem: AwakeSystem<HangZhouMahjong>
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

            if (self.CheckPeng(gamer.HandCards.Where(item => !gamer.OpenDeal.Keys.Contains(item)).ToList(), card) &&
                !self.IsNowPlayer(gamer.PlayerId))
            {
                operate += OperateType.MahjongPeng;
            }

            if (self.CheckGang(gamer.HandCards, card, gamer))
            {
                operate += OperateType.MahjongGang;
            }

            if (self.CheckChi(gamer.HandCards.Where(item => !gamer.OpenDeal.Keys.Contains(item)).ToList(), card) && self.IsNextPlayer(gamer.PlayerId))
            {
                operate += OperateType.MahjongChi;
            }

            if (self.CheckHu(gamer.HandCards, card))
            {
                operate += OperateType.MahjongChi;
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
            int isPeng = handCards.Count(item => item.CardValue == card.CardValue && item.CardType == card.CardType);
            return isPeng >= 2;
        }

        private static bool CheckGang(this HangZhouMahjong self, List<Card> handCards, Card card, Gamer gamer)
        {
            //如果是我出牌。我就不可以杠操作
            if (card.Equals(gamer.OutCards.LastOrDefault()))
            {
                return false;
            }

            var pengAndGang = (from gang in handCards
                select new { item = gang, count = handCards.Count(c => c.CardValue == gang.CardValue && c.CardType == gang.CardType) }).ToArray();

            if (pengAndGang.Where(item => item.count == 3).Select(item => item.item).Contains(card, new CardComparer()))
            {
                return true;
            }

            if (pengAndGang.Any(item => item.count == 4))
            {
                return self.IsNowPlayer(gamer.PlayerId);
            }

            return false;
        }

        private static bool CheckChi(this HangZhouMahjong self, List<Card> handCards, Card card)
        {
            if (card.CardType is CardType.Feng or CardType.Jian)
            {
                return false;
            }

            int count = handCards.Where(item =>
                            item.CardType == card.CardType && math.abs(item.CardValue - card.CardValue) < 2 &&
                            math.abs(item.CardValue - card.CardValue) > 0)
                    .Distinct<Card>(new CardComparer()).Count();

            if (count > 1)
            {
                return true;
            }

            return false;
        }

        private static bool CheckHu(this HangZhouMahjong self, List<Card> handCards, Card card)
        {
            return false;
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
        }

        private static void GamerPengCard(this HangZhouMahjong self, Gamer gamer)
        {
            Round round = self.GetParent<Round>();
            Card last = round.OutCards.First();
            round.OutCards.Remove(last);
            gamer.HandCards.Add(last);
            self.ChangeGamer(gamer, round);

            List<Card> openList = gamer.HandCards.Where(item => item.CardValue == last.CardValue && item.CardType == last.CardType).ToList();
            for (int i = 0; i < 3; i++)
            {
                gamer.OpenDeal.Add(openList[i], OpenDealType.Peng);
            }

            round.Status = RoundStatus.Next;
        }

        private static void GamerGangCard(this HangZhouMahjong self, Gamer gamer, List<Card> operateCards)
        {
            Round round = self.GetParent<Round>();
            int openType = OpenDealType.Gang;
            Card GangCard = round.OutCards.First();

            if (self.IsNowPlayer(gamer.PlayerId))
            {
                if (!gamer.OpenDeal.Keys.Contains(operateCards.First(), new CardComparer()))
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
                gamer.OpenDeal.Add(item, openType);
            }

            Card outCard = round.LibCards.First();
            round.LibCards.Remove(outCard);
            gamer.OutCards.Add(outCard);
            round.OutCards.Add(outCard);

            Card mo = round.LibCards.First();
            round.LibCards.Remove(outCard);
            gamer.HandCards.Add(mo);
            self.ChangeGamer(gamer, round);

            self.Gang = true;
            round.Status = RoundStatus.Next;
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