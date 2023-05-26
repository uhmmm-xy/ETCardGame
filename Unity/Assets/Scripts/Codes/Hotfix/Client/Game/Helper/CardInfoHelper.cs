namespace ET.Client
{
    public static class CardInfoHelper
    {
        public static bool Equals(CardInfo x, CardInfo y)
        {
            if (x == null)
                return false;
            if (y == null)
                return false;
            
            return x.Value == y.Value && x.Type == y.Type;
        }
    }
}