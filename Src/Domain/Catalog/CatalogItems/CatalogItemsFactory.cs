using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.Common.Enums.EnumHelpers;
using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItemsFactory : ICatalogItemsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public CatalogItemsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public ICatalogItem CreateNewCatalogItem(
            IBrandInfo brand,
            ItemNumber itemNumber,
            IScaleInfo scale,
            PowerMethod powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available)
        {
            CatalogItemId id = new CatalogItemId(_guidSource.NewGuid());
            Slug slug = brand.Slug.CombineWith(itemNumber);

            return new CatalogItem(
                id,
                brand,
                itemNumber,
                slug,
                scale,
                powerMethod,
                description,
                prototypeDescription,
                modelDescription,
                deliveryDate,
                available,
                rollingStocks,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public ICatalogItem CatalogItemWith(
            Guid catalogItemId,
            string slug,
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale, string powerMethod, string? deliveryDate, bool available,
            string description, string? modelDescription, string? prototypeDescription,
            IReadOnlyList<IRollingStock> rollingStocks,
            DateTime created, int version)
        {
            CatalogItemId id = new CatalogItemId(catalogItemId);
            Slug itemSlug = Slug.Of(slug);

            return new CatalogItem(
                id,
                brand,
                new ItemNumber(itemNumber),
                itemSlug,
                scale,
                RequiredValueFor<PowerMethod>(powerMethod),
                description,
                prototypeDescription,
                modelDescription,
                DeliveryDate.TryParse(deliveryDate, out var dd) ? dd : null,
                available,
                rollingStocks,
                Instant.FromDateTimeUtc(created),
                null,
                version);
        }

        public ICatalogItem UpdateCatalogItem(
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
            bool? available)
        {
            var itemSlug = item.Slug;

            if (itemNumber.HasValue || brand != null)
            {
                var brandSlug = brand?.Slug ?? item.Brand.Slug;
                itemSlug = brandSlug.CombineWith(itemNumber ?? item.ItemNumber);
            }

            return new CatalogItem(
                item.CatalogItemId,
                brand ?? item.Brand,
                itemNumber ?? item.ItemNumber,
                itemSlug,
                scale ?? item.Scale,
                powerMethod ?? item.PowerMethod,
                description ?? item.Description,
                prototypeDescription ?? item.PrototypeDescription,
                modelDescription ?? item.ModelDescription,
                deliveryDate ?? item.DeliveryDate,
                available ?? item.IsAvailable,
                rollingStocks.Count > 0 ? rollingStocks : item.RollingStocks,
                item.CreatedDate,
                _clock.GetCurrentInstant(),
                item.Version + 1);
        }
    }
}
