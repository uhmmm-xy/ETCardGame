using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GamerComponent : Entity,IAwake,IDestroy
    {
        public List<int> PlayerIds = new();
        public Dictionary<long, Gamer> Gamers = new();
    }
}