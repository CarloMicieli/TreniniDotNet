using TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class NewRemoveRollingStockFromCatalogItemInput
    {
        public static readonly RemoveRollingStockFromCatalogItemInput Empty = With();

        public static RemoveRollingStockFromCatalogItemInput With(
            Slug? slug = null, RollingStockId? rollingStockId = null) =>
            new RemoveRollingStockFromCatalogItemInput(slug ?? Slug.Empty, rollingStockId ?? RollingStockId.Empty);
    }
}
