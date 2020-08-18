using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems
{
    public static class DatabaseTestHelpersExtensions
    {
        public static void ArrangeWithoutCatalogItems(this DatabaseTestHelpers database)
        {
            database.Setup.TruncateTable(Tables.RollingStocks);
            database.Setup.TruncateTable(Tables.CatalogItems);
            database.Setup.TruncateTable(Tables.Brands);
            database.Setup.TruncateTable(Tables.Railways);
            database.Setup.TruncateTable(Tables.Scales);

            var Acme = CatalogSeedData.Brands.Acme;
            var Fs = CatalogSeedData.Railways.Fs;
            var H0 = CatalogSeedData.Scales.H0;

            database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = Acme.Id.ToGuid(),
                name = Acme.Name,
                slug = Acme.Slug.ToString(),
                kind = Acme.Kind.ToString(),
                created = DateTime.UtcNow
            });

            database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = Fs.Id.ToGuid(),
                name = Fs.Name,
                slug = Fs.Slug.ToString(),
                country = Fs.Country.Code,
                created = DateTime.UtcNow
            });

            database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = H0.Id.ToGuid(),
                name = H0.Name,
                slug = H0.Slug.ToString(),
                ratio = H0.Ratio.ToDecimal(),
                gauge_mm = H0.Gauge.InMillimeters.Value,
                gauge_in = H0.Gauge.InInches.Value,
                track_type = H0.Gauge.TrackGauge.ToString(),
                created = DateTime.UtcNow
            });
        }

        public static void ArrangeWithOneCatalogItem(this DatabaseTestHelpers database, CatalogItem catalogItem)
        {
            database.Setup.TruncateTable(Tables.RollingStocks);
            database.Setup.TruncateTable(Tables.CatalogItems);

            database.ArrangeWithoutCatalogItems();

            var catalogItemId = catalogItem.Id.ToGuid();

            database.Arrange.Insert(Tables.CatalogItems, new
            {
                catalog_item_id = catalogItemId,
                brand_id = catalogItem.Brand.Id.ToGuid(),
                scale_id = catalogItem.Scale.Id.ToGuid(),
                item_number = catalogItem.ItemNumber.Value,
                slug = catalogItem.Slug.Value,
                power_method = catalogItem.PowerMethod.ToString(),
                description = catalogItem.Description,
                created = DateTime.UtcNow
            });

            foreach (var rs in catalogItem.RollingStocks)
            {
                switch (rs)
                {
                    case Locomotive l:
                        database.Arrange.Insert(Tables.RollingStocks, new
                        {
                            rolling_stock_id = rs.Id.ToGuid(),
                            catalog_item_id = catalogItemId,
                            category = rs.Category.ToString(),
                            epoch = rs.Epoch.ToString(),
                            railway_id = rs.Railway.Id.ToGuid(),
                            dcc_interface = l.DccInterface.ToString(),
                            control = l.Control.ToString(),
                            class_name = l.Prototype?.ClassName,
                            road_number = l.Prototype?.RoadNumber,
                            series = l.Prototype?.Series
                        });
                        break;
                    case PassengerCar p:
                        database.Arrange.Insert(Tables.RollingStocks, new
                        {
                            rolling_stock_id = rs.Id.ToGuid(),
                            catalog_item_id = catalogItemId,
                            category = rs.Category.ToString(),
                            epoch = rs.Epoch.ToString(),
                            railway_id = rs.Railway.Id.ToGuid(),
                            passenger_car_type = p.PassengerCarType,
                            service_level = p.ServiceLevel,
                            type_name = p.TypeName
                        });
                        break;
                    case Train t:
                        database.Arrange.Insert(Tables.RollingStocks, new
                        {
                            rolling_stock_id = rs.Id.ToGuid(),
                            catalog_item_id = catalogItemId,
                            category = rs.Category.ToString(),
                            epoch = rs.Epoch.ToString(),
                            railway_id = rs.Railway.Id.ToGuid(),
                            dcc_interface = t.DccInterface.ToString(),
                            control = t.Control.ToString(),
                            type_name = t.TypeName
                        });
                        break;

                    default:
                        database.Arrange.Insert(Tables.RollingStocks, new
                        {
                            rolling_stock_id = rs.Id.ToGuid(),
                            catalog_item_id = catalogItemId,
                            category = rs.Category.ToString(),
                            epoch = rs.Epoch.ToString(),
                            railway_id = rs.Railway.Id.ToGuid()
                        });
                        break;
                }
            }
        }
    }
}