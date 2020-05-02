using System;
using System.Collections.Generic;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemsFactory
    {
        ICatalogItem CreateNewCatalogItem(
            IBrandInfo brand,
            ItemNumber itemNumber,
            IScaleInfo scale,
            PowerMethod powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available);

        ICatalogItem UpdateCatalogItem(
            ICatalogItem item,
            IBrandInfo? brand,
            ItemNumber? itemNumber,
            IScaleInfo? scale,
            PowerMethod? powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string? description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool? available);

        ICatalogItem CatalogItemWith(
            Guid catalogItemId,
            string slug,
            IBrandInfo brandInfo, string itemNumber,
            IScaleInfo scaleInfo, string powerMethod, string? deliveryDate, bool available,
            string description, string? modelDescription, string? prototypeDescription,
            IReadOnlyList<IRollingStock> rollingStocks,
            DateTime modified, int version);
    }
}
