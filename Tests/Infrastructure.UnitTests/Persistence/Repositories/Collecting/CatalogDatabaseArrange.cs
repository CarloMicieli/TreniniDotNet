using System;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using CatalogTables = TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Tables;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting
{
    public static class CatalogDatabaseArrange
    {
        public static void ArrangeCatalogData(this DatabaseArrange databaseArrange)
        {
            var acme = CatalogSeedData.Brands.NewAcme();
            var fs = CatalogSeedData.Railways.NewFs();
            var H0 = CatalogSeedData.Scales.ScaleH0();

            var acme60392 = CatalogSeedData.CatalogItems.NewAcme60392();
            var acme60458 = CatalogSeedData.CatalogItems.NewAcme60458();
            
            databaseArrange.InsertOne(CatalogTables.Brands, new
            {
                brand_id = acme.Id.ToGuid(),
                name = acme.Name,
                slug = acme.Slug.ToString(),
                kind = BrandKind.Industrial.ToString(),
                created = DateTime.UtcNow
            });

            databaseArrange.InsertOne(CatalogTables.Railways, new
            {
                railway_id = fs.Id.ToGuid(),
                name = fs.Name,
                slug = fs.Slug.ToString(),
                country = fs.Country.Code,
                created = DateTime.UtcNow
            });

            databaseArrange.InsertOne(CatalogTables.Scales, new
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

            InsertCatalogItem(databaseArrange, acme60392);
            InsertCatalogItem(databaseArrange, acme60458);
        }

        private static void InsertCatalogItem(DatabaseArrange databaseArrange, CatalogItem catalogItem)
        {
            databaseArrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = catalogItem.Id.ToGuid(),
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
                databaseArrange.Insert(CatalogTables.RollingStocks, new
                {
                    rolling_stock_id = rs.Id.ToGuid(),
                    catalog_item_id = catalogItem.Id.ToGuid(),
                    category = rs.Category.ToString(),
                    epoch = rs.Epoch.ToString(),
                    railway_id = rs.Railway.ToString()
                });
            }
        }
    }
}