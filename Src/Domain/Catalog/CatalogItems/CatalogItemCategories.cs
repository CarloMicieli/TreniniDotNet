using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public static class CatalogItemCategories
    {
        private static CatalogItemCategory From(string? cat1, string? cat2)
        {
            var category1 = EnumHelpers.OptionalValueFor<Category>(cat1);
            var category2 = EnumHelpers.OptionalValueFor<Category>(cat2);

            if (category1.HasValue && category2.HasValue)
            {
                if (category1.Value == category2.Value)
                {
                    return FromCategory(category1.Value);
                }

                return CatalogItemCategory.Trains;
            }

            return CatalogItemCategory.Unspecified;
        }

        public static CatalogItemCategory FromCatalogItem(CatalogItem item)
        {
            return item.RollingStocks.Count switch
            {
                0 => CatalogItemCategory.Unspecified,
                1 => FromCategory(item.RollingStocks.First().Category),
                _ => FromCategories(item.RollingStocks.Select(it => it.Category))
            };
        }

        public static CatalogItemCategory FromCategories(IEnumerable<Category> categoriesList)
        {
            var categories = categoriesList
                .Select(FromCategory)
                .Where(it => it != CatalogItemCategory.Unspecified)
                .Distinct()
                .ToList();

            return categories.Count switch
            {
                0 => CatalogItemCategory.Unspecified,
                1 => categories.First(),
                _ => CatalogItemCategory.Trains
            };
        }
        
        private static CatalogItemCategory FromCategory(Category cat)
        {
            if (Categories.IsLocomotive(cat))
            {
                return CatalogItemCategory.Locomotives;
            }

            if (Categories.IsTrain(cat))
            {
                return CatalogItemCategory.Trains;
            }

            if (Categories.IsPassengerCar(cat))
            {
                return CatalogItemCategory.PassengerCars;
            }

            if (Categories.IsFreightCar(cat))
            {
                return CatalogItemCategory.FreightCars;
            }

            return CatalogItemCategory.Unspecified;
        }
    }
}