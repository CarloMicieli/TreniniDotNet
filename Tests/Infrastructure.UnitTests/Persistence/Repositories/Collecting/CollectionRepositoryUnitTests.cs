using System;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using CatalogTables = TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Tables;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting
{
    public abstract class CollectionRepositoryUnitTests<TRepository> : RepositoryUnitTests<TRepository>
    {
        protected CollectionRepositoryUnitTests(SqliteDatabaseFixture fixture, Func<IUnitOfWork, TRepository> builder)
            : base(fixture, builder)
        {
        }

        protected Brand Acme { set; get; }
        protected Railway Fs { set; get; }
        protected Scale H0 { set; get; }

        protected CatalogItem Acme_60392 { set; get; }
        protected CatalogItem Acme_60458 { set; get; }

        protected void ArrangeCatalogData()
        {
            Acme = CatalogSeedData.Brands.NewAcme();
            Fs = CatalogSeedData.Railways.NewFs();
            H0 = CatalogSeedData.Scales.ScaleH0();

            Acme_60392 = CatalogSeedData.CatalogItems.NewAcme60392();
            Acme_60458 = CatalogSeedData.CatalogItems.NewAcme60458();

            Database.Setup.TruncateTable(CatalogTables.RollingStocks);
            Database.Setup.TruncateTable(CatalogTables.CatalogItems);
            Database.Setup.TruncateTable(CatalogTables.Brands);
            Database.Setup.TruncateTable(CatalogTables.Railways);
            Database.Setup.TruncateTable(CatalogTables.Scales);

            Database.Arrange.InsertOne(CatalogTables.Brands, new
            {
                brand_id = Acme.Id.ToGuid(),
                name = Acme.Name,
                slug = Acme.Slug.ToString(),
                kind = BrandKind.Industrial.ToString(),
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(CatalogTables.Railways, new
            {
                railway_id = Fs.Id.ToGuid(),
                name = Fs.Name,
                slug = Fs.Slug.ToString(),
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(CatalogTables.Scales, new
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

            Database.Arrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = Acme_60392.Id.ToGuid(),
                brand_id = Acme.Id.ToGuid(),
                scale_id = H0.Id.ToGuid(),
                item_number = Acme_60392.ItemNumber.Value,
                slug = Acme_60392.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = Acme_60392.Description,
                created = DateTime.UtcNow
            });

            Database.Arrange.Insert(CatalogTables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = Acme_60392.Id.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                era = Epoch.V.ToString(),
                railway_id = Fs.Id.ToGuid()
            });

            Database.Arrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = Acme_60458.Id.ToGuid(),
                brand_id = Acme.Id.ToGuid(),
                scale_id = H0.Id.ToGuid(),
                item_number = "123457",
                slug = Acme_60458.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = "Catalog Item 123457",
                created = DateTime.UtcNow
            });

            Database.Arrange.Insert(CatalogTables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = Acme_60458.Id.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                era = Epoch.V.ToString(),
                railway_id = Fs.Id.ToGuid()
            });
        }
    }
}
