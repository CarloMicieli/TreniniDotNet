using System;
using NodaTime;

namespace TreniniDotNet.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Encodes the provided Date to be SEO friendly
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToSeoFriendly(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static Instant ToUtcOrGetCurrent(this DateTime? dateTime, IClock clock)
        {
            if (dateTime.HasValue)
            {
                DateTime dt = dateTime.Value;
                return Instant.FromUtc(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            }

            return clock.GetCurrentInstant();
        }
    }
}
