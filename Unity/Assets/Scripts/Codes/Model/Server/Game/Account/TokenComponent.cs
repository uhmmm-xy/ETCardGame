using System.Collections.Generic;

namespace ET
{
    public class TokenComponent : Entity,IAwake,IDestroy
    {
        public Dictionary<long, string> TokenDictionary = new();
    }
}