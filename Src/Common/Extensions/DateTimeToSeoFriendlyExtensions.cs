using System;

namespace TreniniDotNet.Common.Extensions
{
    public static class DateTimeToSeoFriendlyExtensions
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
    }
}
