using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Application.Catalog.Railways;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.Application.Catalog.Scales;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Application.Catalog.Scales.EditScale;

namespace TreniniDotNet.Application.TestInputs.Catalog
{
    public static class NewAddressInput
    {
        public static AddressInput NewEmpty() => With();

        public static AddressInput With(
            string Line1 = null,
            string Line2 = null,
            string City = null,
            string Region = null,
            string PostalCode = null,
            string Country = null) => new AddressInput
            {
                Line1 = Line1,
                Line2 = Line2,
                City = City,
                Region = Region,
                PostalCode = PostalCode,
                Country = Country
            };
    }

    public static class CatalogInputs
    {
        public static class NewCreateBrandInput
        {
            public static CreateBrandInput Empty() => With();

            public static CreateBrandInput With(
                string Name = null,
                string CompanyName = null,
                string GroupName = null,
                string Description = null,
                string WebsiteUrl = null,
                string EmailAddress = null,
                string BrandType = null,
                AddressInput Address = null) => new CreateBrandInput(
                    Name,
                    CompanyName,
                    GroupName,
                    Description,
                    WebsiteUrl,
                    EmailAddress,
                    BrandType,
                    Address);
        }

        public static class NewEditBrandInput
        {
            public static EditBrandInput Empty => With();

            public static EditBrandInput With(
                Slug? BrandSlug = null,
                string Name = null,
                string CompanyName = null,
                string GroupName = null,
                string Description = null,
                string WebsiteUrl = null,
                string EmailAddress = null,
                string BrandType = null,
                AddressInput Address = null) => new EditBrandInput(
                    BrandSlug ?? Slug.Empty,
                    Name,
                    CompanyName,
                    GroupName,
                    Description,
                    WebsiteUrl,
                    EmailAddress,
                    BrandType,
                    Address);
        }

        public static class NewCreateRailwayInput
        {
            public static CreateRailwayInput Empty = With();

            public static CreateRailwayInput With(
                string Name = null,
                string CompanyName = null,
                string Country = null,
                PeriodOfActivityInput PeriodOfActivity = null,
                TotalRailwayLengthInput TotalLength = null,
                RailwayGaugeInput Gauge = null,
                string Website = null,
                string Headquarters = null) => new CreateRailwayInput(
                    Name, CompanyName, Country, PeriodOfActivity, TotalLength, Gauge, Website, Headquarters);
        }

        public static class NewEditRailwayInput
        {
            public static EditRailwayInput Empty = With();

            public static EditRailwayInput With(
                Slug? RailwaySlug = null,
                string Name = null,
                string CompanyName = null,
                string Country = null,
                PeriodOfActivityInput PeriodOfActivity = null,
                TotalRailwayLengthInput TotalLength = null,
                RailwayGaugeInput Gauge = null,
                string Website = null,
                string Headquarters = null) => new EditRailwayInput(
                    RailwaySlug ?? Slug.Empty,
                    Name, CompanyName, Country,
                    PeriodOfActivity, TotalLength, Gauge, Website, Headquarters);
        }

        public static class NewPeriodOfActivityInput
        {
            public static PeriodOfActivityInput With(
                string Status = null,
                DateTime? OperatingUntil = null,
                DateTime? OperatingSince = null) =>
                new PeriodOfActivityInput(Status, OperatingSince, OperatingUntil);
        }

        public static class NewTotalRailwayLengthInput
        {
            public static TotalRailwayLengthInput With(
                decimal? Kilometers = null,
                decimal? Miles = null) =>
                new TotalRailwayLengthInput(Kilometers, Miles);
        }

        public static class NewRailwayGaugeInput
        {
            public static RailwayGaugeInput With(
                string TrackGauge = null,
                decimal? Millimeters = null,
                decimal? Inches = null) =>
                new RailwayGaugeInput(TrackGauge, Millimeters, Inches);
        }

        public static class NewCreateScaleInput
        {
            public static CreateScaleInput Empty => With();

            public static CreateScaleInput With(
                string Name = null,
                decimal? Ratio = null,
                ScaleGaugeInput Gauge = null,
                string Description = null,
                List<string> Standards = null,
                int? Weight = null) =>
                new CreateScaleInput(Name, Ratio, Gauge, Description, Standards, Weight);
        }

        public static class NewEditScaleInput
        {
            public static EditScaleInput Empty => With();

            public static EditScaleInput With(
                Slug? ScaleSlug = null,
                string Name = null,
                decimal? Ratio = null,
                ScaleGaugeInput Gauge = null,
                string Description = null,
                List<string> Standards = null,
                int? Weight = null) =>
                new EditScaleInput(ScaleSlug ?? Slug.Empty, Name, Ratio, Gauge, Description, Standards, Weight);
        }

        public static class NewScaleGaugeInput
        {
            public static ScaleGaugeInput With(string TrackGauge = null, decimal? Inches = null, decimal? Millimeters = null) =>
                new ScaleGaugeInput(TrackGauge, Inches, Millimeters);
        }

        public static class NewCreateCatalogItemInput
        {
            public static CreateCatalogItemInput Empty = With();

            public static CreateCatalogItemInput With(
                string brand = null,
                string itemNumber = null,
                string description = null,
                string prototypeDescription = null,
                string modelDescription = null,
                string powerMethod = null,
                string scale = null,
                string deliveryDate = null,
                bool available = true,
                IReadOnlyList<RollingStockInput> rollingStocks = null) =>
                new CreateCatalogItemInput(
                    brand,
                    itemNumber,
                    description, prototypeDescription, modelDescription,
                    powerMethod, scale, deliveryDate, available, rollingStocks);
        }

        public static class NewEditCatalogItemInput
        {
            public static EditCatalogItemInput Empty = With();

            public static EditCatalogItemInput With(
                Slug? itemSlug = null,
                string brand = null,
                string itemNumber = null,
                string description = null,
                string prototypeDescription = null,
                string modelDescription = null,
                string powerMethod = null,
                string scale = null,
                string deliveryDate = null,
                bool available = true,
                IReadOnlyList<RollingStockInput> rollingStocks = null) =>
                new EditCatalogItemInput(
                    itemSlug ?? Slug.Empty,
                    brand,
                    itemNumber,
                    description, prototypeDescription, modelDescription,
                    powerMethod, scale, deliveryDate, available, rollingStocks);
        }

        public static class NewRollingStockInput
        {
            public static RollingStockInput Empty => With();

            public static RollingStockInput With(
                string era = null, string category = null, string railway = null,
                string className = null, string roadNumber = null,
                string typeName = null, string passengerCarType = null, string serviceLevel = null,
                LengthOverBufferInput length = null,
                string control = null, string dccInterface = null) =>
                new RollingStockInput(era, category, railway,
                    className, roadNumber, typeName, passengerCarType, serviceLevel,
                    length,
                    control, dccInterface);
        }
    }
}
