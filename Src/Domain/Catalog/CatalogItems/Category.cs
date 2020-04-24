using System;

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

    public static class Categories
    {
        public static bool IsLocomotive(string category)
        {
            if (TryParse(category, out var cat))
            {
                return IsLocomotive(cat);
            }

            return true;
        }

        public static bool IsLocomotive(Category cat) =>
            cat == Category.SteamLocomotive ||
            cat == Category.ElectricLocomotive ||
            cat == Category.DieselLocomotive;

        public static bool IsTrain(string category)
        {
            if (TryParse(category, out var cat))
            {
                return IsTrain(cat);
            }

            return true;
        }

        public static bool IsTrain(Category cat) =>
            cat == Category.ElectricMultipleUnit ||
            cat == Category.Railcar ||
            cat == Category.TrainSet ||
            cat == Category.StarterSet;

        public static bool IsFreightCar(Category cat) =>
            cat == Category.FreightCar;

        public static bool IsPassengerCar(Category cat) =>
            cat == Category.PassengerCar;

        private static bool TryParse(string str, out Category result)
        {
            if (string.IsNullOrWhiteSpace(str) == false && Enum.TryParse<Category>(str, true, out var r))
            {
                result = r;
                return true;
            }

            result = default;
            return false;
        }
    }
}
