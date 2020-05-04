using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItem : IModifiableEntity, ICatalogItemInfo
    {
        IReadOnlyList<IRollingStock> RollingStocks { get; }

        string Description { get; }

        string? PrototypeDescription { get; }

        string? ModelDescription { get; }

        DeliveryDate? DeliveryDate { get; }

        bool IsAvailable { get; }

        IScaleInfo Scale { get; }

        PowerMethod PowerMethod { get; }

        ICatalogItemInfo ToCatalogItemInfo();

        ICatalogItem With(
            IBrandInfo? brand = null,
            ItemNumber? itemNumber = null,
            IScaleInfo? scale = null,
            PowerMethod? powerMethod = null,
            string? description = null,
            string? prototypeDescription = null,
            string? modelDescription = null,
            DeliveryDate? deliveryDate = null,
            bool? available = null,
            IReadOnlyList<IRollingStock>? rollingStocks = null)
        {
            var catalogItemSlug = Slug;
            if (brand != null && itemNumber != null)
            {
                catalogItemSlug = brand.Slug.CombineWith(itemNumber.Value);
            }

            return new CatalogItem(
                CatalogItemId,
                brand ?? Brand,
                itemNumber ?? ItemNumber,
                catalogItemSlug,
                scale ?? Scale,
                powerMethod ?? PowerMethod,
                description ?? Description,
                prototypeDescription ?? PrototypeDescription,
                modelDescription ?? ModelDescription,
                deliveryDate ?? DeliveryDate,
                available ?? IsAvailable,
                rollingStocks ?? RollingStocks,
                CreatedDate,
                ModifiedDate,
                Version);
        }
    }
}
