using System;
using System.Collections.Generic;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItemsFactory : ICatalogItemsFactory
    {
        public ICatalogItem NewCatalogItem(Guid catalogItemId, Guid brandId, string itemNumber, string slug, string powerMethod, string? deliveryDate, string description, string? modelDescription, string? prototypeDescription, List<IRollingStock> list, DateTime? createdAt, int? version)
        {
            throw new NotImplementedException();
        }

        public IRollingStock NewRollingStock(Guid rollingStockId, Guid railwayId, string era, string category, decimal? length, string? className, string? roadNumber)
        {
            throw new NotImplementedException();
        }
    }
}
