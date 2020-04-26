using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.Common.Enums.EnumHelpers;
using System;
using TreniniDotNet.Common.DeliveryDates;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItemsFactory : ICatalogItemsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public CatalogItemsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock;
            _guidSource = guidSource;
        }

        public ICatalogItem CreateNewCatalogItem(
            IBrandInfo brand,
            ItemNumber itemNumber,
            IScaleInfo scale,
            PowerMethod powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string description,
            string? prototypeDescr,
            string? modelDescr,
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
                prototypeDescr,
                modelDescr,
                deliveryDate,
                available,
                rollingStocks,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public IRollingStock NewLocomotive(
            IRailwayInfo railway, string era, string category,
            LengthOverBuffer? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control)
        {
            return NewRollingStock(
                _guidSource.NewGuid(),
                railway: railway,
                era: era,
                category: category,
                length: length,
                className: className,
                roadNumber: roadNumber,
                dccInterface: dccInterface,
                control: control);
        }

        public IRollingStock NewTrain(
            IRailwayInfo railway, string era, string category,
            LengthOverBuffer? length,
            string? className, string? roadNumber,
            string? dccInterface, string? control)
        {
            return NewRollingStock(
                _guidSource.NewGuid(),
                railway: railway,
                era: era,
                category: category,
                length: length,
                className: className,
                roadNumber: roadNumber,
                dccInterface: dccInterface,
                control: control);
        }

        public IRollingStock NewRollingStock(
            IRailwayInfo railway, string era, string category,
            LengthOverBuffer? length,
            string? typeName)
        {
            return NewRollingStock(
                _guidSource.NewGuid(),
                railway: railway,
                era: era,
                category: category,
                length: length,
                typeName: typeName);
        }

        public IRollingStock NewRollingStock(
            Guid id,
            IRailwayInfo railway,
            string era,
            string category,
            LengthOverBuffer? length,
            string? className = null, string? roadNumber = null, string? typeName = null,
            string? dccInterface = null, string? control = null)
        {
            RollingStockId rollingStockId = new RollingStockId(id);

            return new RollingStock(
                rollingStockId,
                railway,
                RequiredValueFor<Category>(category),
                RequiredValueFor<Era>(era),
                length,
                className,
                roadNumber,
                typeName,
                OptionalValueFor<DccInterface>(dccInterface) ?? DccInterface.None,
                OptionalValueFor<Control>(control) ?? Control.None);
        }

        public ICatalogItem NewCatalogItem(
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
    }
}
