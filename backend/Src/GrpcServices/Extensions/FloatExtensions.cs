namespace TreniniDotNet.GrpcServices.Extensions
{
    public static class FloatExtensions
    {
        public static decimal? ToNullableDecimal(this float? f)
        {
            if (f.HasValue)
                return (decimal)f;

            return null;
        }

        public static decimal ToDecimal(this float f)
        {
            return (decimal)f;
        }
    }
}
