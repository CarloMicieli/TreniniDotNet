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
}