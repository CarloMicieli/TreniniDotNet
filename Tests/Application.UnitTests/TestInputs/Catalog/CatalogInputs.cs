using System;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Application.TestInputs.Catalog
{
    public static class CatalogInputs
    {
        public static class NewBrandInput
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

        public static class NewRailwayInput
        {
            public static CreateRailwayInput NewEmpty() => With();

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

        public static class NewScaleInput
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

        public static class NewScaleGaugeInput
        {
            public static ScaleGaugeInput With(string TrackGauge = null, decimal? Inches = null, decimal? Millimeters = null) =>
                new ScaleGaugeInput(TrackGauge, Inches, Millimeters);
        }

        public static class NewCatalogItemInput
        {
            public static CreateCatalogItemInput Empty = With();

            public static CreateCatalogItemInput With(
                string BrandName = null,
                string ItemNumber = null,
                string Description = null,
                string PrototypeDescription = null,
                string ModelDescription = null,
                string PowerMethod = null,
                string Scale = null,
                string DeliveryDate = null,
                bool Available = true,
                IList<RollingStockInput> RollingStocks = null) =>
                new CreateCatalogItemInput(BrandName, ItemNumber, Description, PrototypeDescription, ModelDescription,
                    PowerMethod, Scale, DeliveryDate, Available, RollingStocks);
        }

        public static class NewRollingStockInput
        {
            public static RollingStockInput Empty => With();

            public static RollingStockInput With(
                string Era = null, string Category = null, string Railway = null,
                string ClassName = null, string RoadNumber = null, string TypeName = null,
                LengthOverBufferInput Length = null,
                string Control = null, string DccInterface = null) =>
                new RollingStockInput(Era, Category, Railway,
                    ClassName, RoadNumber, TypeName, Length,
                    Control, DccInterface);
        }
    }
}