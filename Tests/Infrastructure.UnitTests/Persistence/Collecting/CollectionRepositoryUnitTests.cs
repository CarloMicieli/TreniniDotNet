using System;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using CatalogTables = TreniniDotNet.Infrastructure.Persistence.Catalog.Tables;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting
{
    public abstract class CollectionRepositoryUnitTests<TRepository> : RepositoryUnitTests<TRepository>
    {
        protected CollectionRepositoryUnitTests(SqliteDatabaseFixture fixture, Func<IDatabaseContext, IClock, TRepository> builder)
            : base(fixture, builder)
        {
        }

        protected IBrandInfo Acme { set; get; }
        protected IRailwayInfo Fs { set; get; }
        protected IScaleInfo H0 { set; get; }

        protected ICatalogRef Acme_123456 { set; get; }
        protected ICatalogRef Acme_123457 { set; get; }

        protected void ArrangeCatalogData()
        {
            this.Acme = new TestBrandInfo
            {
                BrandId = BrandId.NewId(),
                Slug = Slug.Of("ACME"),
                Name = "ACME"
            };

            this.Fs = new TestRailwayInfo
            {
                RailwayId = RailwayId.NewId(),
                Slug = Slug.Of("FS"),
                Name = "FS",
                Country = Country.Of("IT")
            };

            this.H0 = new TestScaleInfo
            {
                ScaleId = ScaleId.NewId(),
                Name = "H0",
                Ratio = Ratio.Of(87M),
                Slug = Slug.Of("h0")
            };

            this.Acme_123456 = CatalogRef.Of(CatalogItemId.NewId(), "acme-123456");
            this.Acme_123457 = CatalogRef.Of(CatalogItemId.NewId(), "acme-123457");

            Database.Setup.TruncateTable(CatalogTables.RollingStocks);
            Database.Setup.TruncateTable(CatalogTables.CatalogItems);
            Database.Setup.TruncateTable(CatalogTables.Brands);
            Database.Setup.TruncateTable(CatalogTables.Railways);
            Database.Setup.TruncateTable(CatalogTables.Scales);

            Database.Arrange.InsertOne(CatalogTables.Brands, new
            {
                brand_id = Acme.BrandId.ToGuid(),
                name = Acme.Name,
                slug = Acme.Slug.ToString(),
                kind = BrandKind.Industrial.ToString(),
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(CatalogTables.Railways, new
            {
                railway_id = Fs.RailwayId.ToGuid(),
                name = Fs.Name,
                slug = Fs.Slug.ToString(),
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(CatalogTables.Scales, new
            {
                scale_id = H0.ScaleId.ToGuid(),
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
                catalog_item_id = Acme_123456.CatalogItemId.ToGuid(),
                brand_id = Acme.BrandId.ToGuid(),
                scale_id = H0.ScaleId.ToGuid(),
                item_number = "123456",
                slug = Acme_123456.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = "Catalog Item 123456",
                created = DateTime.UtcNow
            });

            Database.Arrange.Insert(CatalogTables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = Acme_123456.CatalogItemId.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                era = Epoch.V.ToString(),
                railway_id = Fs.RailwayId.ToGuid()
            });

            Database.Arrange.Insert(CatalogTables.CatalogItems, new
            {
                catalog_item_id = Acme_123457.CatalogItemId.ToGuid(),
                brand_id = Acme.BrandId.ToGuid(),
                scale_id = H0.ScaleId.ToGuid(),
                item_number = "123457",
                slug = Acme_123457.Slug.Value,
                power_method = PowerMethod.DC.ToString(),
                description = "Catalog Item 123457",
                created = DateTime.UtcNow
            });

            Database.Arrange.Insert(CatalogTables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = Acme_123457.CatalogItemId.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                era = Epoch.V.ToString(),
                railway_id = Fs.RailwayId.ToGuid()
            });
        }
    }

    internal class TestBrandInfo : IBrandInfo
    {
        public BrandId BrandId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; }
    }

    internal class TestRailwayInfo : IRailwayInfo
    {
        public RailwayId RailwayId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; }

        public Country Country { set; get; }
    }

    internal class TestScaleInfo : IScaleInfo
    {
        public ScaleId ScaleId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; }

        public Ratio Ratio { set; get; }
    }
}
