using TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class NewAddRollingStockToCatalogItemInput
    {
        public static readonly AddRollingStockToCatalogItemInput Empty = With();

        public static AddRollingStockToCatalogItemInput With(
            Slug? itemSlug = null,
            RollingStockInput rollingStock = null) =>
            new AddRollingStockToCatalogItemInput(itemSlug ?? Slug.Empty, rollingStock ?? NewRollingStockInput.Empty);
    }
}
