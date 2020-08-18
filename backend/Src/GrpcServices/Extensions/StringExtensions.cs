namespace TreniniDotNet.GrpcServices.Extensions
{
    public static class StringExtensions
    {
        public static string? ToNullableString(this string str) =>
            string.IsNullOrWhiteSpace((str)) ? null : str;
    }
}
