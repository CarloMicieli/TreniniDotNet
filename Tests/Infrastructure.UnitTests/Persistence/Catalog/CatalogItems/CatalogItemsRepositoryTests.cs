using System;
using System.Threading.Tasks;
using NodaTime;
using Xunit;
using FluentAssertions;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using System.Linq;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public class CatalogItemsRepositoryTests : RepositoryUnitTests<ICatalogItemRepository>
    {
        private readonly IBrandInfo brand;
        private readonly IRailwayInfo railway;
        private readonly IScaleInfo scale;

        public CatalogItemsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
            this.brand = new BrandInfo(BrandId.NewId(), Slug.Of("ACME"), "ACME");
            this.railway = new FakeRailwayInfo();
            this.scale = new FakeScaleInfo();

            Database.Setup.TruncateTable(Tables.Brands);
            Database.Setup.TruncateTable(Tables.Railways);
            Database.Setup.TruncateTable(Tables.Scales);

            Database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = brand.BrandId.ToGuid(),
                name = brand.Name,
                slug = brand.Slug.ToString(),
                kind = BrandKind.Industrial.ToString(),
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = railway.RailwayId.ToGuid(),
                name = railway.Name,
                slug = railway.Slug.ToString(),
                country = "IT",
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scale.ScaleId.ToGuid(),
                name = scale.Name,
                slug = scale.Slug.ToString(),
                ratio = scale.Ratio.ToDecimal(),
                gauge_mm = 16.5M,
                gauge_in = 0.65M,
                track_type = TrackGauge.Standard.ToString(),
                created = DateTime.UtcNow
            });
        }

        private static ICatalogItemRepository CreateRepository(IDatabaseContext databaseContext, IClock clock)
            => new CatalogItemRepository(
                databaseContext,
                new CatalogItemsFactory(clock, FakeGuidSource.NewSource(Guid.NewGuid())));

        [Fact]
        public async Task CatalogItemRepository_Add_ShouldCreateNewCatalogItems()
        {
            Database.Setup.TruncateTable(Tables.RollingStocks);
            Database.Setup.TruncateTable(Tables.CatalogItems);

            var catalogItem = new FakeCatalogItem(brand);
            var catalogItemId = await Repository.AddAsync(catalogItem);

            catalogItemId.Should().Be(catalogItem.CatalogItemId);

            Database.Assert.RowInTable(Tables.CatalogItems)
                .WithPrimaryKey(new
                {
                    catalog_item_id = catalogItemId.ToGuid()
                })
                .ShouldExists();

            Database.Assert.RowInTable(Tables.RollingStocks)
                .WithPrimaryKey(new
                {
                    catalog_item_id = catalogItemId.ToGuid(),
                    rolling_stock_id = catalogItem.RollingStocks[0].RollingStockId.ToGuid()
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CatalogItemRepository_Exists_ShouldCheckCatalogItemExists()
        {
            Database.ArrangeWithOneCatalogItem(
                catalogItemId: new CatalogItemId(Guid.NewGuid()),
                brandId: brand.BrandId,
                railwayId: railway.RailwayId,
                scaleId: scale.ScaleId);

            var exists = await Repository.ExistsAsync(brand, new ItemNumber("123456"));

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task CatalogItemRepository_Exists_ShouldReturnFalseWhenCatalogItemDoesNotExist()
        {
            Database.ArrangeWithOneCatalogItem(
                catalogItemId: new CatalogItemId(Guid.NewGuid()),
                brandId: brand.BrandId,
                railwayId: railway.RailwayId,
                scaleId: scale.ScaleId);

            var exists = await Repository.ExistsAsync(brand, new ItemNumber("654321"));

            exists.Should().BeFalse();
        }

        [Fact]
        public async Task CatalogItemRepository_GetBySlug_ShouldReturnsCatalogItem()
        {
            Database.ArrangeWithOneCatalogItem(
                catalogItemId: new CatalogItemId(Guid.NewGuid()),
                brandId: brand.BrandId,
                railwayId: railway.RailwayId,
                scaleId: scale.ScaleId);

            var slug = Slug.Of("acme", "123456");
            var catalogItem = await Repository.GetBySlugAsync(slug);

            catalogItem.Should().NotBeNull();
            catalogItem.Slug.Should().Be(slug);
        }

        [Fact]
        public async Task CatalogItemRepository_GetBySlug_ShouldReturnsNullWhenCatalogItemIsNotFound()
        {
            Database.ArrangeWithOneCatalogItem(
                catalogItemId: new CatalogItemId(Guid.NewGuid()),
                brandId: brand.BrandId,
                railwayId: railway.RailwayId,
                scaleId: scale.ScaleId);

            var slug = Slug.Of("acme", "654321");
            var catalogItem = await Repository.GetBySlugAsync(slug);

            catalogItem.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemRepository_GetByBrandAndItemNumber_ShouldReturnsCatalogItem()
        {
            Database.ArrangeWithOneCatalogItem(
                catalogItemId: new CatalogItemId(Guid.NewGuid()),
                brandId: brand.BrandId,
                railwayId: railway.RailwayId,
                scaleId: scale.ScaleId);

            var catalogItem = await Repository.GetByBrandAndItemNumberAsync(brand, new ItemNumber("123456"));

            catalogItem.Should().NotBeNull();
            catalogItem.Brand.Should().Be(brand);
            catalogItem.ItemNumber.Should().Be(new ItemNumber("123456"));
        }

        [Fact]
        public async Task CatalogItemRepository_GetByBrandAndItemNumber_ShouldReturnsNullWhenCatalogItemIsNotFound()
        {
            Database.ArrangeWithOneCatalogItem(
                catalogItemId: new CatalogItemId(Guid.NewGuid()),
                brandId: brand.BrandId,
                railwayId: railway.RailwayId,
                scaleId: scale.ScaleId);

            var catalogItem = await Repository.GetByBrandAndItemNumberAsync(brand, new ItemNumber("654321"));

            catalogItem.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemRepository_UpdateAsync_ShouldUpdateCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            Database.ArrangeWithOneCatalogItem(
                catalogItemId: item.CatalogItemId,
                brandId: item.Brand.BrandId,
                railwayId: item.RollingStocks.First().Railway.RailwayId,
                scaleId: item.Scale.ScaleId);

            await Repository.UpdateAsync(item);

            Database.Assert.RowInTable(Tables.CatalogItems)
                .WithPrimaryKey(new
                {
                    catalog_item_id = item.CatalogItemId.ToGuid()
                })
                .AndValues(new
                {
                    version = 2,
                });
        }
    }

    public static class DatabaseTestHelpersExtensions
    {
        public static void ArrangeWithOneCatalogItem(this DatabaseTestHelpers db,
            CatalogItemId catalogItemId,
            BrandId brandId,
            RailwayId railwayId,
            ScaleId scaleId)
        {
            db.Setup.TruncateTable(Tables.RollingStocks);
            db.Setup.TruncateTable(Tables.CatalogItems);

            db.Arrange.Insert(Tables.CatalogItems, new
            {
                catalog_item_id = catalogItemId.ToGuid(),
                brand_id = brandId.ToGuid(),
                scale_id = scaleId.ToGuid(),
                item_number = "123456",
                slug = "acme-123456",
                power_method = "dc",
                description = "",
                created = DateTime.UtcNow
            });

            db.Arrange.Insert(Tables.RollingStocks, new
            {
                rolling_stock_id = Guid.NewGuid(),
                catalog_item_id = catalogItemId.ToGuid(),
                category = Category.ElectricLocomotive.ToString(),
                era = Epoch.V.ToString(),
                railway_id = railwayId.ToGuid()
            });
        }
    }
}
