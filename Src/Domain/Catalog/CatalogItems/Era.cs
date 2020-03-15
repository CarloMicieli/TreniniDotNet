namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    /// <summary>
    /// The model railways Era enumeration.
    /// </summary>
    public enum Era
    {
        /// <summary>
        /// Era I: country & private railways.
        /// </summary>
        I,

        /// <summary>
        /// Era II: the period after the formation of large state railways.
        /// </summary>
        II,

        /// <summary>
        /// Era III: the new organization of European railroads.
        /// </summary>
        III,

        /// <summary>
        /// Era IV: standardized computer lettering on all rolling stock.
        /// </summary>
        IV,

        /// <summary>
        /// Era V: the modern era of railroading.
        /// </summary>
        V,

        /// <summary>
        /// Era VI
        /// </summary>
        VI
    }

    public static class EraExtensions
    {
        public static Era? ToEra(this string? s)
        {
            if (System.Enum.TryParse<Era>(s, true, out Era result))
                return result;

            return null;
        }
    }
}
