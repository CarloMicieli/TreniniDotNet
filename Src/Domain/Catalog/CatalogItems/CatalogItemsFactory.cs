using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItemsFactory : ICatalogItemsFactory
    {
        public ICatalogItem NewCatalogItem(Guid catalogItemId, IBrandInfo brand, string itemNumber, string slug, 
            IScaleInfo scale, 
            string powerMethod, 
            string? deliveryDate, 
            string description, string? modelDescription, string? prototypeDescription, 
            List<IRollingStock> rollingStocks, 
            DateTime? createdAt, 
            int? version)
        {
            return new CatalogItem(
                new CatalogItemId(catalogItemId),
                brand,
                new ItemNumber(itemNumber),
                Slug.Of(slug),
                scale,
                rollingStocks,
                powerMethod.ToPowerMethod() ?? PowerMethod.None,
                description,
                prototypeDescription,
                modelDescription);
        }

        public IRollingStock NewRollingStock(Guid rollingStockId, 
            IRailwayInfo railway, 
            string era, string category, 
            decimal? length,
            string? className, string? roadNumber)
        {
            return new RollingStock(
                new RollingStockId(rollingStockId),
                railway,
                category.ToCategory() ?? Category.DieselLocomotive,
                era.ToEra() ?? Era.I,
                length.HasValue ? Length.Of(length.Value) : Length.Of(0M), //TODO: fixme
                category,
                roadNumber);
        }
    }
}
