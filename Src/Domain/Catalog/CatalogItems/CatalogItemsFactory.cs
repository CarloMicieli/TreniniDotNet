using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItemsFactory : AggregateRootFactory<CatalogItemId, CatalogItem>
    {
        public CatalogItemsFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public CatalogItem CreateCatalogItem(
            Brand brand,
            ItemNumber itemNumber,
            Scale scale,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available,
            IEnumerable<RollingStock> rollingStocks)
        {
            return new CatalogItem(
                NewId(id => new CatalogItemId(id)),
                brand,
                itemNumber,
                scale,
                powerMethod,
                description,
                prototypeDescription,
                modelDescription,
                deliveryDate,
                available,
                rollingStocks,
                GetCurrentInstant(),
                null,
                1);
        }
    }
}
