namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    /// <summary>
    /// The enumeration of the model categories.
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// The steam locomotives category
        /// </summary>
        SteamLocomotive,

        /// <summary>
        /// The diesel locomotives category
        /// </summary>
        DieselLocomotive,

        /// <summary>
        /// The electric locomotives category
        /// </summary>
        ElectricLocomotive,

        /// <summary>
        /// The railcar category
        /// </summary>
        Railcar,

        /// <summary>
        /// The electric multiple unit category
        /// </summary>
        ElectricMultipleUnit,

        /// <summary>
        /// The freight cars category
        /// </summary>
        FreightCar,

        /// <summary>
        /// The passenger cars category
        /// </summary>
        PassengerCar,

        /// <summary>
        /// The train set category
        /// </summary>
        TrainSet,

        /// <summary>
        /// The starter sets (usually includes the tracks) category
        /// </summary>
        StarterSet
    }

    public static class CategoryExtensions
    {
        public static Category? ToCategory(this string? s)
        {
            if (System.Enum.TryParse<Category>(s, true, out Category result))
                return result;

            return null;
        }
    }
}
