using System.Collections.Generic;

namespace ET
{
    [ChildOf(typeof(RoundComponent))]
    public class Round: Entity, IAwake<List<Card>,List<long>,int,int>
    {
        public List<Card> LibCards = new (); //底牌
        public List<Card> OutCards = new (); //弃牌
        public List<int> Score; //分数
        public List<long> Players; //玩家

        public int PlayerIndex; //当前回合玩家
        public int GameType;
        public RoundStatus Status;
    }

    public enum RoundStatus
    {
        WaitOperate,//等待操作
        Next, //继续回合
        Over, //回合结束
    }
}