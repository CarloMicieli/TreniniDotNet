using System;
using NodaTime;

namespace TreniniDotNet.Common.Extensions
{
    public static class DateTimeExtensions
    {
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

        public static Instant ToUtc(this DateTime dt) =>
            Instant.FromUtc(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

        public static Instant? ToUtcOrDefault(this DateTime? dt) =>
            (dt is null) ? (Instant?)null : dt.Value.ToUtc();

        public static LocalDate ToLocalDate(this DateTime dateTime) =>
            LocalDateTime.FromDateTime(dateTime).Date;

        public static LocalDate? ToLocalDateOrDefault(this DateTime? dateTime) =>
            (dateTime is null) ? (LocalDate?)null : dateTime.Value.ToLocalDate();
    }
}
