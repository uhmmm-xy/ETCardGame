using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(Gamer))]
    [FriendOf(typeof(User))]
    public static class GamerHelper
    {
        public static List<GamerInfo> GamerToGamerInfo(Scene scene, List<int> playerids)
        {
            List<GamerInfo> infos = new();
            foreach (int id in playerids)
            {
                Gamer gamer = scene.GetComponent<GamerComponent>().GetPlayer(id);
                infos.Add(ToInfo(gamer));
            }

            return infos;
        }

        public static GamerInfo ToInfo(Gamer self)
        {
            GamerInfo info = new();
            info.PlayerId = self.PlayerId;
            User user = self.GetComponent<Account>().GetComponent<User>();
            info.HandImage = user.HandImage;
            info.Name = "";
            info.Status = self.Status;
            info.Score = self.Score;
            info.OpenDeal = CardHelper.CardToCardInfo(self.OpenDeal);
            info.OutCards = CardHelper.CardToCardInfo(self.OutCards);
            info.HandCards = CardHelper.CardToCardInfo(self.HandCards);
            return info;
        }
    }
}