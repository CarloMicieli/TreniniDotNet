using System;

namespace TreniniDotNet.SharedKernel.Slugs
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
    }
}