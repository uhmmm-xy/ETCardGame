﻿using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Account))]
    public class Gamer : Entity,IAwake<int,int,long>,IDestroy
    {
        public List<Card> HandCards = new ();//手牌
        public List<Card> OutCards = new();//弃牌
        public int Score; //分数

        public Dictionary<Card, int> Operate = new();

        //public List<Card> OperateCards;//可以操作的牌

        //public List<int> Operates; //可以操作的集合

        public int OperateType; //操作类型

        public int Status; //玩家状态

        public int RoomId;//所属房间ID

        public int PlayerId;//玩家ID

        public long PlayerSessionId;//玩家会话ID
    }
    
}