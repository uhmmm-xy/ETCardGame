using System.Collections.Generic;

namespace ET
{
    [ComponentOf]
    public class CardComponent : Entity,IAwake,IAwake<int[]>,IDestroy
    {
        public List<Card> Cards =new ();
    }
}