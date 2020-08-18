using System;
using System.Linq;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using CatalogTables = TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Tables;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting
{
    public static class CatalogDatabaseTestHelpers
    {
        public static void ArrangeCatalogData(this DatabaseTestHelpers database, CatalogItem catalogItem)
        {
            var brand = catalogItem.Brand;
            var scale = catalogItem.Scale;

            database.Arrange.InsertOne(CatalogTables.Brands, new
            {
                brand_id = brand.Id.ToGuid(),
                name = brand.ToString(),
                slug = brand.Slug.ToString(),
                kind = BrandKind.Industrial.ToString(),
                created = DateTime.UtcNow
            });

            database.Arrange.InsertOne(CatalogTables.Scales, new
            {
                scale_id = scale.Id.ToGuid(),
                name = scale.ToString(),
                slug = scale.Slug.ToString(),
                ratio = 87M,
                gauge_mm = 16.5M,
                gauge_in = 0.65M,
                track_type = TrackGauge.Standard.ToString(),
                created = DateTime.UtcNow
            });

            database.Arrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = catalogItem.Id.ToGuid(),
                brand_id = brand.Id.ToGuid(),
                scale_id = scale.Id.ToGuid(),
                item_number = catalogItem.ItemNumber.Value,
                slug = catalogItem.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = catalogItem.Description,
                created = DateTime.UtcNow
            });

            foreach (var rs in catalogItem.RollingStocks)
            {
                database.Arrange.InsertOne(CatalogTables.Railways, new
                {
                    railway_id = rs.Id.ToGuid(),
                    name = rs.Railway.ToString(),
                    slug = rs.Railway.Slug,
                    created = DateTime.UtcNow
                });

                database.Arrange.Insert(CatalogTables.RollingStocks, new
                {
                    rolling_stock_id = Guid.NewGuid(),
                    catalog_item_id = catalogItem.Id.ToGuid(),
                    category = Category.ElectricLocomotive.ToString(),
                    epoch = Epoch.V.ToString(),
                    railway_id = rs.Railway.Id.ToGuid()
                });
            }
        }

        public static void ArrangeCatalogData(this DatabaseTestHelpers database)
        {
            var acme = CatalogSeedData.Brands.NewAcme();
            var fs = CatalogSeedData.Railways.NewFs();
            var H0 = CatalogSeedData.Scales.ScaleH0();

            var acme60392 = CatalogSeedData.CatalogItems.NewAcme60392();
            var acme60458 = CatalogSeedData.CatalogItems.NewAcme60458();

            database.Setup.TruncateTable(CatalogTables.RollingStocks);
            database.Setup.TruncateTable(CatalogTables.CatalogItems);
            database.Setup.TruncateTable(CatalogTables.Brands);
            database.Setup.TruncateTable(CatalogTables.Railways);
            database.Setup.TruncateTable(CatalogTables.Scales);

            database.Arrange.InsertOne(CatalogTables.Brands, new
            {
                brand_id = acme.Id.ToGuid(),
                name = acme.Name,
                slug = acme.Slug.ToString(),
                kind = BrandKind.Industrial.ToString(),
                created = DateTime.UtcNow
            });

            database.Arrange.InsertOne(CatalogTables.Railways, new
            {
                railway_id = fs.Id.ToGuid(),
                name = fs.Name,
                slug = fs.Slug.ToString(),
                created = DateTime.UtcNow
            });

            database.Arrange.InsertOne(CatalogTables.Scales, new
            {
                scale_id = H0.Id.ToGuid(),
                name = H0.Name,
                slug = H0.Slug.ToString(),
                ratio = H0.Ratio.ToDecimal(),
                gauge_mm = 16.5M,
                gauge_in = 0.65M,
                track_type = TrackGauge.Standard.ToString(),
                created = DateTime.UtcNow
            });

            database.Arrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = acme60392.Id.ToGuid(),
                brand_id = acme.Id.ToGuid(),
                scale_id = H0.Id.ToGuid(),
                item_number = acme60392.ItemNumber.Value,
                slug = acme60392.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = acme60392.Description,
                created = DateTime.UtcNow
            });

            database.Arrange.Insert(CatalogTables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = acme60392.Id.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                epoch = Epoch.V.ToString(),
                railway_id = fs.Id.ToGuid()
            });

            database.Arrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = acme60458.Id.ToGuid(),
                brand_id = acme.Id.ToGuid(),
                scale_id = H0.Id.ToGuid(),
                item_number = "123457",
                slug = acme60458.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = "Catalog Item 123457",
                created = DateTime.UtcNow
            });

            database.Arrange.Insert(CatalogTables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = acme60458.Id.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                epoch = Epoch.V.ToString(),
                railway_id = fs.Id.ToGuid()
            });
        }
    }
}
