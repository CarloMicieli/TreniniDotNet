namespace TreniniDotNet.Common.Extensions
{
    public static class DecimalExtensions
    {
        public static bool IsPositive(this decimal d)
        {
            return d > 0M;
        }

        public static bool IsNegative(this decimal d)
        {
            return d < 0M;
        }

        public static float ToFloat(this decimal d)
        {
            return (float)d;
        }
    }
}