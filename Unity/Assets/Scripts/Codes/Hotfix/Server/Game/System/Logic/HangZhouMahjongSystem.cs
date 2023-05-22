using System;
using System.Collections.Generic;
using System.Linq;
using ET.Server.EventType;

namespace ET
{
    [FriendOf(typeof (Gamer))]
    [FriendOf(typeof (Card))]
    [FriendOfAttribute(typeof (ET.Round))]
    public static class HangZhouMahjongSystem
    {
        public class HangZhouMahjongAwakeSystem: AwakeSystem<HangZhouMahjong>
        {
            protected override void Awake(HangZhouMahjong self)
            {
            }
        }

        public static void DealCard(this HangZhouMahjong self, Gamer gamer)
        {
            Round round = self.GetParent<Round>();
            List<Card> cards = round.LibCards.GetRange(0, 13);
            cards = cards.SortCard();
            round.LibCards.RemoveRange(0, 13);
            gamer.HandCards = cards;
            self.CheckPlayerOperate(gamer);
            EventSystem.Instance.Publish(self.DomainScene(),
                new SendPlayerMessage() { Player = gamer, Message = new M2C_DealCard() { Cards = GamerHelper.ToInfo(gamer).HandCards } });
        }

        public static void RoundStart(this HangZhouMahjong self)
        {
            Round round = self.GetParent<Round>();
            Card card = round.LibCards.First();
            round.LibCards.Remove(card);
            Gamer player = self.DomainScene().GetComponent<GamerComponent>().GetPlayer(round.Players[round.PlayerIndex]);
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
            player.HandCards.Add(card);
            EventSystem.Instance.Publish(self.DomainScene(),
                new SendPlayerMessage() { Player = player, Message = new M2C_MoCard() { Card = card.ToInfo() } });
        }

        static void CheckPlayerOperate(this HangZhouMahjong self, Gamer gamer)
        {
            GameRoom room = self.DomainScene().GetComponent<GameRoomComponent>().GetRoom(gamer.RoomId);
            Round round = self.GetParent<Round>();
            List<Card> allCard = room.GetComponent<CardComponent>().GetCard().Where(card => !round.OutCards.Contains(card)).ToList();

            Card tem = null;

            foreach (Card card in gamer.HandCards)
            {
                if (tem is null)
                {
                    tem = card;
                    continue;
                }

                if (tem.CardType != card.CardType)
                {
                    tem = card;
                    continue;
                }

                if (tem.CardValue == card.CardValue)
                {
                    List<Card> select = allCard.FindAll(item => item.CardValue.Equals(tem.CardValue) && item.CardType.Equals(tem.CardType));
                    AddOperateCard(gamer, select, OperateType.MahjongPeng);
                    tem = card;
                    continue;
                }

                if (self.CheckChi(tem, card))
                {
                    self.AddChiCard(tem, card, allCard, gamer);
                }

                tem = card;
            }
        }

        public static void AddOperateCard(Gamer self, List<Card> cards, int type)
        {
            cards.ForEach(item => { AddOperateCardEach(self, item, type); });
        }

        private static void AddOperateCardEach(Gamer self, Card card, int type)
        {
            if (self.Operate.ContainsKey(card))
            {
                if (type == self.Operate[card])
                {
                    return;
                }

                self.Operate[card] += type;
                return;
            }

            self.Operate.Add(card, type);
        }

        private static bool CheckChi(this HangZhouMahjong self, Card _out, Card _in)
        {
            if (_in.CardType is CardType.Feng or CardType.Jian || _in.CardType != _out.CardType)
            {
                return false;
            }

            int differ = Math.Abs(_out.CardValue - _in.CardValue);

            if (differ is 0 or > 3)
            {
                return false;
            }

            return true;
        }

        private static void AddChiCard(this HangZhouMahjong self, Card inCard, Card outCard, List<Card> allCard, Gamer player)
        {
            if (inCard.CardValue > outCard.CardValue)
            {
                if (inCard.CardValue - outCard.CardValue == 1)
                {
                    List<Card> select = allCard.FindAll(item =>
                            item.CardType == inCard.CardType && (item.CardValue == inCard.CardValue + 1 || item.CardValue == outCard.CardValue - 1));
                    AddOperateCard(player, select, OperateType.MahjongChi);
                }

                if (inCard.CardValue - outCard.CardValue == 2)
                {
                    List<Card> select = allCard.FindAll(item =>
                            item.CardType == inCard.CardType && item.CardValue == inCard.CardValue - 1);
                    AddOperateCard(player, select, OperateType.MahjongChi);
                }
            }
            else
            {
                if (outCard.CardValue - inCard.CardValue == 1)
                {
                    List<Card> select = allCard.FindAll(item =>
                            item.CardType == inCard.CardType &&
                            (item.CardValue == (outCard.CardValue + 1) || item.CardValue == (inCard.CardValue - 1)));
                    AddOperateCard(player, select, OperateType.MahjongChi);
                }

                if (outCard.CardValue - inCard.CardValue == 2)
                {
                    List<Card> select = allCard.FindAll(item =>
                            item.CardType == inCard.CardType && item.CardValue == outCard.CardValue - 1);
                    AddOperateCard(player, select, OperateType.MahjongChi);
                }
            }
        }
    }
}