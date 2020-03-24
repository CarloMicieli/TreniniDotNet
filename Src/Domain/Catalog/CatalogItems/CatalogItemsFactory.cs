using System;
using System.Collections.Generic;
using LanguageExt;
using static LanguageExt.Prelude;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using NodaTime;
using TreniniDotNet.Common.Uuid;
using System.Collections.Immutable;

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

        public Validation<Error, ICatalogItem> NewCatalogItem(
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod,
            string? deliveryDate, bool available,
            string description, string? prototypeDescr, string? modelDescr,
            IRollingStock rollingStock)
        {
            return NewCatalogItem(
                brand, itemNumber,
                scale,
                powerMethod,
                deliveryDate, available,
                description, prototypeDescr, modelDescr,
                ImmutableList.Create<IRollingStock>(rollingStock));
        }

        public Validation<Error, ICatalogItem> NewCatalogItem(
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod,
            string? deliveryDate, bool available,
            string description, string? modelDescr, string? prototypeDescr,
            IReadOnlyList<IRollingStock> rollingStock)
        {
            var toItemNumberV = ToItemNumber(itemNumber);
            var toPowerMethodV = ToPowerMethod(powerMethod);
            var toDeliveryDateV = ToDeliveryDate(deliveryDate);

            return (toItemNumberV, toPowerMethodV, toDeliveryDateV).Apply((_itemNumber, _powerMethod, _deliveryDate) =>
            {
                var id = new CatalogItemId(_guidSource.NewGuid());

                ICatalogItem item = new CatalogItem(
                    id,
                    brand,
                    _itemNumber,
                    Slug.Of(brand.Slug.ToString(), _itemNumber.ToString()),
                    scale,
                    _powerMethod,
                    description,
                    prototypeDescr,
                    modelDescr,
                    _deliveryDate,
                    available,
                    rollingStock,
                    _clock.GetCurrentInstant(),
                    1
                );
                return item;
            });
        }

        public Validation<Error, ICatalogItem> HydrateCatalogItem(
            Guid catalogItemId,
            string slug,
            IBrandInfo brand, string itemNumber,
            IScaleInfo scale,
            string powerMethod,
            string? deliveryDate, bool available,
            string description, string? modelDescr, string? prototypeDescr,
            IReadOnlyList<IRollingStock> rollingStocks,
            DateTime lastModifiedAt, int version)
        {
            var toItemNumberV = ToItemNumber(itemNumber);
            var toPowerMethodV = ToPowerMethod(powerMethod);
            var toDeliveryDateV = ToDeliveryDate(deliveryDate);

            return (toItemNumberV, toPowerMethodV, toDeliveryDateV).Apply((_itemNumber, _powerMethod, _deliveryDate) =>
            {
                var id = new CatalogItemId(catalogItemId);

                ICatalogItem item = new CatalogItem(
                    id,
                    brand,
                    _itemNumber,
                    Slug.Of(slug),
                    scale,
                    _powerMethod,
                    description,
                    prototypeDescr,
                    modelDescr,
                    _deliveryDate,
                    available,
                    rollingStocks,
                    Instant.FromDateTimeUtc(lastModifiedAt),
                    version
                );
                return item;
            });
        }

        private static Validation<Error, ItemNumber> ToItemNumber(string str) =>
            ItemNumber.TryCreate(str, out var itemNumber) ? Success<Error, ItemNumber>(itemNumber) : Fail<Error, ItemNumber>(Error.New($"'{str}' is not a valid item number"));

        private static Validation<Error, PowerMethod> ToPowerMethod(string str) =>
            PowerMethods.TryParse(str, out var pm) ? Success<Error, PowerMethod>(pm) : Fail<Error, PowerMethod>(Error.New($"'{str}' is not a valid power method"));

        private static Validation<Error, DeliveryDate?> ToDeliveryDate(string? str)
        {
            if (DeliveryDate.TryParseOpt(str, out var dd))
            {
                return Success<Error, DeliveryDate?>(dd);
            }

            return Fail<Error, DeliveryDate?>(Error.New($"'{str}' is not a valid delivery date"));
        }
    }
}
