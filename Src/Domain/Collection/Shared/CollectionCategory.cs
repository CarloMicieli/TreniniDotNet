using System.Linq;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public enum CollectionCategory
    {
        Unspecified,
        Trains,
        Locomotives,
        PassengerCars,
        FreightCars
    }

    public static class CollectionCategories
    {
        public static CollectionCategory FromCatalogItem(ICatalogItem item)
        {
            if (item.RollingStocks.Count == 0)
            {
                return CollectionCategory.Unspecified;
            }

            if (item.RollingStocks.Count == 1)
            {
                return FromCategory(item.RollingStocks.First().Category);
            }

            var categories = item.RollingStocks
                .Select(it => it.Category)
                .Select(it => FromCategory(it))
                .Where(it => it != CollectionCategory.Unspecified)
                .Distinct()
                .ToList();

            if (categories.Count == 0)
            {
                return CollectionCategory.Unspecified;
            }

            if (categories.Count == 1)
            {
                return categories.First();
            }

            return CollectionCategory.Trains;
        }

        public static CollectionCategory FromCategory(Category cat)
        {
            if (Categories.IsLocomotive(cat))
                return CollectionCategory.Locomotives;

            if (Categories.IsTrain(cat))
                return CollectionCategory.Trains;

            if (Categories.IsPassengerCar(cat))
                return CollectionCategory.PassengerCars;

            if (Categories.IsFreightCar(cat))
                return CollectionCategory.FreightCars;

            return CollectionCategory.Unspecified;
        }
    }
}
