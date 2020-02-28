using System;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public enum RailwayStatus
    {
        Active,
        Inactive
    }

    public static class StringToRailwayStatusExtensions
    {
        /// <summary>
        /// Try to convert this <em>String</em> to one of the <em>RailwayStatus</em>
        /// allowed values.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static RailwayStatus? ToRailwayStatus(this string? s)
        {
            if (!string.IsNullOrEmpty(s) && Enum.TryParse<RailwayStatus>(s, false, out RailwayStatus result))
            {
                return result;
            }

            return null;
        }
    }
}
