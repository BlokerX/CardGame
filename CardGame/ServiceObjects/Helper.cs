namespace CardGame.ServiceObjects
{
    internal static class Helper
    {
        public static string IntToThreeCharStringComparer(this int value, int charPlacesCount = 3)
        {
            if (value < 0) return "∞";

            char[] c = new char[charPlacesCount];

            int d = 1;
            for (int i = charPlacesCount - 1; i >= 0; i--)
            {
                c[i] = char.Parse((value / d % 10).ToString());
                d *= 10;
            }
            return new string(c);
        }
    }
}
