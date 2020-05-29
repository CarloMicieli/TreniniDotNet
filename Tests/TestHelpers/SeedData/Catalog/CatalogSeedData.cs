using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public static class CatalogSeedData
    {
        public static Brands Brands = new Brands();
        public static Railways Railways = new Railways();
        public static Scales Scales = new Scales();
        public static CatalogItems CatalogItems = new CatalogItems();

        public static IBrand NewBrandWith(
            BrandId? brandId = null,
            string name = null,
            string companyName = null,
            string groupName = null,
            string description = null,
            Uri websiteUrl = null,
            MailAddress mailAddress = null,
            Address address = null,
            BrandKind? brandKind = BrandKind.Industrial)
        {
            return new Brand(
                brandId ?? BrandId.NewId(),
                name ?? "ACME",
                Slug.Of(name ?? "ACME"),
                companyName ?? "Anonima Costruzione Modelli Esatti",
                groupName,
                description,
                websiteUrl ?? new Uri("http://www.acmetreni.com"),
                mailAddress ?? new MailAddress("mail@acmetreni.com"),
                brandKind ?? BrandKind.Industrial,
                address ?? Address.With(
                    "address line 1", "address line 2",
                    "city name",
                    "region",
                    "postal code",
                    "IT"),
                Instant.FromUtc(1988, 11, 25, 9, 0),
                null,
                1);
        }

        public static ICatalogItem NewCatalogItemWith(
            CatalogItemId? id = null,
            IBrandInfo brand = null,
            IScaleInfo scale = null,
            ItemNumber? itemNumber = null,
            PowerMethod? powerMethod = null,
            string description = null,
            string prototypeDescription = null,
            string modelDescription = null,
            DeliveryDate? deliveryDate = null,
            bool available = true,
            IReadOnlyList<IRollingStock> rollingStocks = null)
        {
            return new CatalogItem(
                id ?? CatalogItemId.NewId(),
                brand,
                itemNumber ?? new ItemNumber("123456"),
                brand.Slug.CombineWith(itemNumber ?? new ItemNumber("123456")),
                scale,
                powerMethod ?? PowerMethod.None,
                description,
                prototypeDescription,
                modelDescription,
                 deliveryDate,
                available,
                rollingStocks,
                Instant.FromUtc(1988, 11, 25, 9, 0),
                null,
                1);
        }

        public static IRailway NewRailwayWith(
            RailwayId? id = null,
            string name = null,
            string companyName = null,
            Country? country = null,
            PeriodOfActivity periodOfActivity = null,
            RailwayLength railwayLength = null,
            RailwayGauge gauge = null,
            Uri websiteUrl = null,
            string headquarters = null)
        {
            return new Railway(
                id ?? RailwayId.NewId(),
                Slug.Of(name),
                name,
                companyName,
                country,
                periodOfActivity ?? PeriodOfActivity.Default(),
                railwayLength,
                gauge,
                websiteUrl,
                headquarters,
                Instant.FromUtc(1988, 11, 25, 9, 0),
                null,
                1);
        }

        public static IRollingStock NewRollingStockWith(
            RollingStockId? rollingStockId = null,
            IRailwayInfo railway = null,
            Category? category = null,
            Epoch epoch = null,
            LengthOverBuffer length = null,
            string className = null, string roadNumber = null, string typeName = null,
            Couplers? couplers = null,
            string livery = null,
            PassengerCarType? passengerCarType = null,
            ServiceLevel serviceLevel = null,
            DccInterface dccInterface = DccInterface.None,
            Control control = Control.None)
        {
            return new RollingStock(
                rollingStockId ?? RollingStockId.NewId(),
                railway,
                category ?? Category.PassengerCar,
                epoch,
                length,
                className,
                roadNumber,
                typeName,
                couplers,
                livery,
                passengerCarType,
                serviceLevel,
                dccInterface,
                control);
        }

        public static IScale NewScaleWith(
            ScaleId? id = null,
            string name = null,
            Ratio? ratio = null,
            ScaleGauge gauge = null,
            string description = null,
            IImmutableSet<ScaleStandard> standards = null,
            int? weight = null)
        {
            return new Scale(
                id ?? ScaleId.NewId(),
                name,
                Slug.Of(name),
                ratio ?? Ratio.Of(87M),
                gauge,
                description,
                standards ?? ImmutableHashSet<ScaleStandard>.Empty,
                weight,
                Instant.FromUtc(1988, 11, 25, 9, 0),
                null,
                1);
        }
    }
}
