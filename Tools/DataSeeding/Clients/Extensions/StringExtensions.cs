using System;
using System.Globalization;
using Google.Protobuf.WellKnownTypes;

namespace DataSeeding.Clients.Extensions
{
    public static class StringExtensions
    {
        public static Timestamp ToTimestamp(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            if (DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                return Timestamp.FromDateTime(DateTime.SpecifyKind(dt, DateTimeKind.Utc));
            }

            return null;
        }

        public static string ToStringOrBlank(this string s) => s ?? string.Empty;
    }
}
