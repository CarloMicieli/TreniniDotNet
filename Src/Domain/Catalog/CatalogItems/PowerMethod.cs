using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    /// <summary>
    /// The power methods for the model.
    /// </summary>
    public enum PowerMethod
    {
        /// <summary>
        /// Direct current.
        /// </summary>
        DC,

        /// <summary>
        /// Alternating current (Maerklin).
        /// </summary>
        AC,

        /// <summary>
        /// No power method - the value to set for carriages
        /// </summary>
        None
    }

    public static class PowerMethodExtensions
    {
        public static PowerMethod? ToPowerMethod(this string? s)
        {
            if (Enum.TryParse<PowerMethod>(s, true, out PowerMethod result))
                return result;

            return null;
        }
    }

    public static class PowerMethods
    {
        public static bool TryParse(string str, out PowerMethod result)
        {
            if (!string.IsNullOrWhiteSpace(str) && Enum.TryParse<PowerMethod>(str, true, out var pm))
            {
                result = pm;
                return true;
            }

            result = default;
            return false;
        }
    }
}
