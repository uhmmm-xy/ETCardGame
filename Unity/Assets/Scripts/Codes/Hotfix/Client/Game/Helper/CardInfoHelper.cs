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
            if (x.Value.Equals(y.Value) && x.Type.Equals(y.Type))
            {
                return true;
            }
            return false;
        }
    }
}