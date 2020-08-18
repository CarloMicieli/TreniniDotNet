using System.Collections.Generic;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public static class NewCreateCatalogItemInput
    {
        public static readonly CreateCatalogItemInput Empty = With();

        public static CreateCatalogItemInput With(
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
            new CreateCatalogItemInput(
                brand,
                itemNumber,
                description, prototypeDescription, modelDescription,
                powerMethod, scale, deliveryDate, available, rollingStocks);
    }
}
