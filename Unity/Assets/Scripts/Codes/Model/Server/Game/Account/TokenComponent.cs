using System.Collections.Generic;

namespace ET
{
    [ComponentOf]
    public class TokenComponent : Entity,IAwake,IDestroy
    {
        public Dictionary<long, string> TokenDictionary = new();
    }
}