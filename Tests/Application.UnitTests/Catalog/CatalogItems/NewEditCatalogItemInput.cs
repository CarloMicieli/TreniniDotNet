using System.Collections.Generic;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class NewEditCatalogItemInput
    {
        public static readonly EditCatalogItemInput Empty = With();

        public static EditCatalogItemInput With(
            Slug? itemSlug = null,
            string brand = null,
            string itemNumber = null,
            string description = null,
            string prototypeDescription = null,
            string modelDescription = null,
            string powerMethod = null,
            string scale = null,
            string deliveryDate = null,
            bool available = true,
            IReadOnlyList<RollingStockInput> rollingStocks = null) =>
            new EditCatalogItemInput(
                itemSlug ?? Slug.Empty,
                brand,
                itemNumber,
                description, prototypeDescription, modelDescription,
                powerMethod, scale, deliveryDate, available, rollingStocks);
    }
}
