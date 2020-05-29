using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem;
using TreniniDotNet.Application.Catalog.Railways;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.Application.Catalog.Scales;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog
{
    public static class NewAddressInput
    {
        public static readonly AddressInput Empty = With();

        public static AddressInput With(
            string line1 = null,
            string line2 = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null) => new AddressInput
            {
                Line1 = line1,
                Line2 = line2,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country
            };
    }

    public static class CatalogInputs
    {
        public static class NewCreateBrandInput
        {
            public static readonly CreateBrandInput Empty = With();

            public static CreateBrandInput With(
                string name = null,
                string companyName = null,
                string groupName = null,
                string description = null,
                string websiteUrl = null,
                string emailAddress = null,
                string brandType = null,
                AddressInput address = null) => new CreateBrandInput(
                    name,
                    companyName,
                    groupName,
                    description,
                    websiteUrl,
                    emailAddress,
                    brandType,
                    address);
        }

        public static class NewEditBrandInput
        {
            public static readonly EditBrandInput Empty = With();

            public static EditBrandInput With(
                Slug? brandSlug = null,
                string name = null,
                string companyName = null,
                string groupName = null,
                string description = null,
                string websiteUrl = null,
                string emailAddress = null,
                string brandType = null,
                AddressInput address = null) => new EditBrandInput(
                    brandSlug ?? Slug.Empty,
                    name,
                    companyName,
                    groupName,
                    description,
                    websiteUrl,
                    emailAddress,
                    brandType,
                    address);
        }

        public static class NewCreateRailwayInput
        {
            public static readonly CreateRailwayInput Empty = With();

            public static CreateRailwayInput With(
                string name = null,
                string companyName = null,
                string country = null,
                PeriodOfActivityInput periodOfActivity = null,
                TotalRailwayLengthInput totalLength = null,
                RailwayGaugeInput gauge = null,
                string website = null,
                string headquarters = null) => new CreateRailwayInput(
                    name, companyName, country, periodOfActivity, totalLength, gauge, website, headquarters);
        }

        public static class NewEditRailwayInput
        {
            public static readonly EditRailwayInput Empty = With();

            public static EditRailwayInput With(
                Slug? railwaySlug = null,
                string name = null,
                string companyName = null,
                string country = null,
                PeriodOfActivityInput periodOfActivity = null,
                TotalRailwayLengthInput totalLength = null,
                RailwayGaugeInput railwayGauge = null,
                string website = null,
                string headquarters = null) => new EditRailwayInput(
                    railwaySlug ?? Slug.Empty,
                    name, companyName, country,
                    periodOfActivity, totalLength, railwayGauge, website, headquarters);
        }

        public static class NewPeriodOfActivityInput
        {
            public static PeriodOfActivityInput With(
                string status = null,
                DateTime? operatingUntil = null,
                DateTime? operatingSince = null) =>
                new PeriodOfActivityInput(status, operatingSince, operatingUntil);
        }

        public static class NewTotalRailwayLengthInput
        {
            public static TotalRailwayLengthInput With(
                decimal? kilometers = null,
                decimal? miles = null) =>
                new TotalRailwayLengthInput(kilometers, miles);
        }

        public static class NewRailwayGaugeInput
        {
            public static RailwayGaugeInput With(
                string trackGauge = null,
                decimal? millimeters = null,
                decimal? inches = null) =>
                new RailwayGaugeInput(trackGauge, millimeters, inches);
        }

        public static class NewCreateScaleInput
        {
            public static readonly CreateScaleInput Empty = With();

            public static CreateScaleInput With(
                string name = null,
                decimal? ratio = null,
                ScaleGaugeInput scaleGauge = null,
                string description = null,
                List<string> standards = null,
                int? weight = null) =>
                new CreateScaleInput(name, ratio, scaleGauge, description, standards, weight);
        }

        public static class NewEditScaleInput
        {
            public static readonly EditScaleInput Empty = With();

            public static EditScaleInput With(
                Slug? scaleSlug = null,
                string name = null,
                decimal? ratio = null,
                ScaleGaugeInput gauge = null,
                string description = null,
                List<string> standards = null,
                int? weight = null) =>
                new EditScaleInput(scaleSlug ?? Slug.Empty, name, ratio, gauge, description, standards, weight);
        }

        public static class NewScaleGaugeInput
        {
            public static ScaleGaugeInput With(string trackGauge = null, decimal? inches = null, decimal? millimeters = null) =>
                new ScaleGaugeInput(trackGauge, inches, millimeters);
        }

        public static class NewCreateCatalogItemInput
        {
            public static readonly CreateCatalogItemInput Empty = With();

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
            public static readonly EditCatalogItemInput Empty = With();

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
            public static readonly RollingStockInput Empty = With();

            public static RollingStockInput With(
                string epoch = null, string category = null, string railway = null,
                string className = null, string roadNumber = null, string typeName = null,
                string couplers = null,
                string livery = null, string passengerCarType = null, string serviceLevel = null,
                LengthOverBufferInput length = null,
                string control = null, string dccInterface = null) =>
                new RollingStockInput(epoch, category, railway,
                    className, roadNumber, typeName, couplers, livery, passengerCarType, serviceLevel,
                    length,
                    control, dccInterface);
        }

        public static class NewAddRollingStockToCatalogItemInput
        {
            public static readonly AddRollingStockToCatalogItemInput Empty = With();

            public static AddRollingStockToCatalogItemInput With(
                Slug? itemSlug = null,
                RollingStockInput rollingStock = null) =>
                new AddRollingStockToCatalogItemInput(itemSlug ?? Slug.Empty, rollingStock ?? NewRollingStockInput.Empty);
        }

        public static class NewEditRollingStockInput
        {
            public static readonly EditRollingStockInput Empty = With();

            public static EditRollingStockInput With(
                Slug? slug = null, RollingStockId? rollingStockId = null,
                string epoch = null, string category = null, string railway = null,
                string className = null, string roadNumber = null,
                string typeName = null,
                string couplers = null,
                string livery = null, string passengerCarType = null, string serviceLevel = null,
                LengthOverBufferInput length = null,
                string control = null, string dccInterface = null) =>
                new EditRollingStockInput(
                    slug ?? Slug.Empty,
                    rollingStockId ?? RollingStockId.Empty,
                    epoch, category, railway,
                    className, roadNumber, typeName, passengerCarType, serviceLevel,
                    couplers,
                    livery,
                    length,
                    control, dccInterface);
        }

        public static class NewRemoveRollingStockFromCatalogItemInput
        {
            public static readonly RemoveRollingStockFromCatalogItemInput Empty = With();

            public static RemoveRollingStockFromCatalogItemInput With(
                Slug? slug = null, RollingStockId? rollingStockId = null) =>
                new RemoveRollingStockFromCatalogItemInput(slug ?? Slug.Empty, rollingStockId ?? RollingStockId.Empty);
        }
    }
}
