namespace TreniniDotNet.Domain.Catalog.Brands
{
    /// <summary>
    /// The kinds for railway models brands
    /// </summary>
    public enum BrandKind
    {
        /// <summary>
        /// these manufactures produce models using the die casting method
        /// </summary>
        Industrial,

        /// <summary>
        /// these manufacturers produce models which are made of brass or similar alloys.
        ///
        /// They are usually more expensive than the industrial series due to the limited
        /// production quantities and the "hand made" nature of the production
        /// </summary>
        BrassModels
    }
}