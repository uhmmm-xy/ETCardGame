using System.Collections.Generic;

namespace ET
{
    
    [ComponentOf(typeof(GameRoom))]
    public class RoundComponent : Entity,IAwake
    {
        public const int StartIndex = -1;
        public List<Round> Rounds = new();
        public int RoundIndex;
    }
}