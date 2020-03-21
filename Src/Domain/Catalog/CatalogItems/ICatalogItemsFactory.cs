using System;
using System.Collections.Generic;
using LanguageExt;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemsFactory
    {
        Validation<Error, ICatalogItem> NewCatalogItemV(IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod, string? deliveryDate, bool available,
            string description, string? modelDescription, string? prototypeDescription);






        IRollingStock NewRollingStock(Guid rollingStockId, IRailwayInfo railway, string era, string category, decimal? length, string? className, string? roadNumber);

        ICatalogItem NewCatalogItem(Guid catalogItemId, IBrandInfo brand, string itemNumber, string slug,
            IScaleInfo scale,
            string powerMethod, string? deliveryDate,
            string description, string? modelDescription, string? prototypeDescription,
            List<IRollingStock> list, DateTime? createdAt, int? version);
    }
}
