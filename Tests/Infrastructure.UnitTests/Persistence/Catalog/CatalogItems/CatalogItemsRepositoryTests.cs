using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Database.Testing;
using NodaTime;
using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Common;
using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;
using System.Threading.Tasks;
using System.Collections.Immutable;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Infrastructure.Dapper;

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
            this.railway = new TestRailwayInfo();
            this.scale = new TestScaleInfo();

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

            var catalogItem = new TestCatalogItem(brand);
            var catalogItemId = await Repository.Add(catalogItem);

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

            var catalogItem = await Repository.GetByAsync(brand, new ItemNumber("123456"));

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

            var catalogItem = await Repository.GetByAsync(brand, new ItemNumber("654321"));

            catalogItem.Should().BeNull();
        }

        public class TestCatalogItem : ICatalogItem
        {
            public TestCatalogItem(IBrandInfo brand)
            {
                CatalogItemId = new CatalogItemId(Guid.NewGuid());

                RollingStocks = ImmutableList.Create<IRollingStock>(
                    new TestRollingStock(Guid.NewGuid()));

                Brand = brand;
            }

            public CatalogItemId CatalogItemId { get; }

            public IBrandInfo Brand { get; }

            public Slug Slug => Slug.Of("acme", "123456");

            public ItemNumber ItemNumber => new ItemNumber("123456");

            public IReadOnlyList<IRollingStock> RollingStocks { get; }

            public string Description => "Description";

            public string PrototypeDescription => null;

            public string ModelDescription => null;

            public IScaleInfo Scale => new TestScaleInfo();

            public PowerMethod PowerMethod => PowerMethod.DC;

            public DeliveryDate? DeliveryDate => null;

            public bool IsAvailable => false;

            public Instant CreatedDate => Instant.FromUtc(1988, 11, 25, 9, 0);

            public Instant? ModifiedDate => null;

            public int Version => 1;

            public ICatalogItemInfo ToCatalogItemInfo() => this;
        }

        public class TestRollingStock : IRollingStock
        {
            private RollingStockId _id;

            public TestRollingStock(Guid id)
            {
                _id = new RollingStockId(id);
            }

            public RollingStockId RollingStockId => _id;

            public IRailwayInfo Railway => new TestRailwayInfo();

            public Category Category => Category.ElectricLocomotive;

            public Era Era => Era.IV;

            public Length Length => Length.OfMillimeters(210M);

            public string ClassName => "class name";

            public string RoadNumber => "road num";

            public string TypeName => "type name";

            public DccInterface DccInterface => DccInterface.Nem651;

            public Control Control => Control.DccReady;

            Length? IRollingStock.Length => null;
        }

        public class TestRailwayInfo : IRailwayInfo
        {
            private readonly RailwayId railwayId = new RailwayId(Guid.NewGuid());

            public RailwayId RailwayId => railwayId;

            public Slug Slug => Slug.Of("FS");

            public string Name => "FS";

            public Country Country => Country.Of("IT");

            public IRailwayInfo ToRailwayInfo() => this;
        }

        public class TestScaleInfo : IScaleInfo
        {
            private readonly ScaleId scaleId = new ScaleId(Guid.NewGuid());

            public ScaleId ScaleId => scaleId;

            public Slug Slug => Slug.Of("H0");

            public string Name => "H0";

            public Ratio Ratio => Ratio.Of(87M);

            public IScaleInfo ToScaleInfo() => this;
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
                era = Era.V.ToString(),
                railway_id = railwayId.ToGuid()
            });
        }
    }
}
