using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems
{
    public class CatalogItemsRepositoryTests : DapperRepositoryUnitTests<ICatalogItemsRepository>
    {
        private Brand Acme { get; }
        private Railway Fs { get; }
        private Scale H0 { get; }

        public CatalogItemsRepositoryTests()
            : base(unitOfWork => new CatalogItemsRepository(unitOfWork))
        {
            Acme = CatalogSeedData.Brands.Acme;
            Fs = CatalogSeedData.Railways.Fs;
            H0 = CatalogSeedData.Scales.H0;

            Database.Setup.TruncateTable(Tables.Brands);
            Database.Setup.TruncateTable(Tables.Railways);
            Database.Setup.TruncateTable(Tables.Scales);

            Database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = Acme.Id.ToGuid(),
                name = Acme.Name,
                slug = Acme.Slug.ToString(),
                kind = Acme.Kind.ToString(),
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = Fs.Id.ToGuid(),
                name = Fs.Name,
                slug = Fs.Slug.ToString(),
                country = Fs.Country.Code,
                created = DateTime.UtcNow
            });

            Database.Arrange.InsertOne(Tables.Scales, new
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

        [Fact]
        public async Task CatalogItemsRepository_AddAsync_ShouldCreateNewCatalogItems()
        {
            Database.Setup.TruncateTable(Tables.RollingStocks);
            Database.Setup.TruncateTable(Tables.CatalogItems);

            var catalogItem = CatalogSeedData.CatalogItems.Acme60392;
            var catalogItemId = await Repository.AddAsync(catalogItem);
            await UnitOfWork.SaveAsync();

            catalogItemId.Should().Be(catalogItem.Id);

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
                    rolling_stock_id = catalogItem.RollingStocks[0].Id.ToGuid()
                })
                .ShouldExists();
        }


        [Fact]
        public async Task CatalogItemsRepository_ExistsAsync_ShouldCheckCatalogItemExists()
        {
            var item = CatalogSeedData.CatalogItems.Acme60392;
            Database.ArrangeWithOneCatalogItem(item);

            var exists = await Repository.ExistsAsync(Acme, item.ItemNumber);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task CatalogItemsRepository_ExistsAsync_ShouldReturnFalseWhenCatalogItemDoesNotExist()
        {
            Database.ArrangeWithOneCatalogItem(CatalogSeedData.CatalogItems.Acme60392);

            var exists = await Repository.ExistsAsync(Acme, new ItemNumber("654321"));

            exists.Should().BeFalse();
        }

        [Fact]
        public async Task CatalogItemsRepository_GetBySlugAsync_ShouldReturnsCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.Acme60392;
            Database.ArrangeWithOneCatalogItem(item);

            var catalogItem = await Repository.GetBySlugAsync(item.Slug);

            catalogItem.Should().NotBeNull();
            catalogItem?.Slug.Should().Be(item.Slug);
        }

        [Fact]
        public async Task CatalogItemsRepository_GetBySlugAsync_ShouldReturnsNullWhenCatalogItemIsNotFound()
        {
            Database.ArrangeWithOneCatalogItem(CatalogSeedData.CatalogItems.Acme60392);

            var slug = Slug.Of("acme", "654321");
            var catalogItem = await Repository.GetBySlugAsync(slug);

            catalogItem.Should().BeNull();
        }

        // [Fact]
        // public async Task CatalogItemRepository_GetByBrandAndItemNumber_ShouldReturnsCatalogItem()
        // {
        //     var item = CatalogSeedData.CatalogItems.Acme_60392();
        //     Database.ArrangeWithOneCatalogItem(item);
        //
        //     var catalogItem = await Repository.GetByBrandAndItemNumberAsync(_brand, item.ItemNumber);
        //
        //     catalogItem.Should().NotBeNull();
        //     catalogItem.Brand.Slug.Should().Be(_brand.Slug);
        //     catalogItem.ItemNumber.Should().Be(item.ItemNumber);
        // }

        // [Fact]
        // public async Task CatalogItemRepository_GetByBrandAndItemNumber_ShouldReturnsNullWhenCatalogItemIsNotFound()
        // {
        //     Database.ArrangeWithOneCatalogItem(CatalogSeedData.CatalogItems.Acme60392);
        //
        //     var catalogItem = await Repository.GetByBrandAndItemNumberAsync(_brand, new ItemNumber("654321"));
        //
        //     catalogItem.Should().BeNull();
        // }

        [Fact]
        public async Task CatalogItemRepository_UpdateAsync_ShouldUpdateCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.Acme60392;
            Database.ArrangeWithOneCatalogItem(item);

            var modified = item.With(
                description: "Modified description");

            await Repository.UpdateAsync(modified);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.CatalogItems)
                .WithPrimaryKey(new
                {
                    catalog_item_id = item.Id.ToGuid()
                })
                .AndValues(new
                {
                    description = "Modified description"
                });
        }

        // [Fact]
        // public async Task CatalogItemRepository_AddRollingStockAsync_ShouldAddNewRollingStocks()
        // {
        //     var item = CatalogSeedData.CatalogItems.Acme_60392();
        //     Database.ArrangeWithOneCatalogItem(item);
        //
        //     var rollingStock = CatalogSeedData.NewRollingStockWith(
        //         RollingStockId.NewId(),
        //         _railway,
        //         Category.DieselLocomotive,
        //         Epoch.III);
        //
        //     await Repository.AddRollingStockAsync(item, rollingStock);
        //
        //     Database.Assert.RowInTable(Tables.RollingStocks)
        //         .WithPrimaryKey(new
        //         {
        //             catalog_item_id = item.Id.ToGuid(),
        //             rolling_stock_id = rollingStock.Id.ToGuid()
        //         })
        //         .AndValues(new
        //         {
        //             epoch = "III",
        //             category = rollingStock.Category.ToString()
        //         });
        // }

        // [Fact]
        // public async Task CatalogItemRepository_UpdateRollingStockAsync_ShouldAddNewRollingStocks()
        // {
        //     var item = CatalogSeedData.CatalogItems.Acme_60392();
        //     Database.ArrangeWithOneCatalogItem(item);
        //
        //     var rollingStock = item.RollingStocks.First().With(control: Control.DccSound);
        //
        //     await Repository.UpdateRollingStockAsync(item, item.RollingStocks.First());
        //
        //     Database.Assert.RowInTable(Tables.RollingStocks)
        //         .WithPrimaryKey(new
        //         {
        //             catalog_item_id = item.Id.ToGuid(),
        //             rolling_stock_id = rollingStock.Id.ToGuid()
        //         })
        //         .AndValues(new
        //         {
        //             control = "DccSound"
        //         });
        // }

        // [Fact]
        // public async Task CatalogItemRepository_DeleteRollingStockAsync_ShouldAddNewRollingStocks()
        // {
        //     var item = CatalogSeedData.CatalogItems.Acme_60392();
        //     Database.ArrangeWithOneCatalogItem(item);
        //
        //     var rollingStock = item.RollingStocks.First().With(control: Control.DccSound);
        //
        //     await Repository.DeleteRollingStockAsync(item, item.RollingStocks.First().Id);
        //
        //     Database.Assert.RowInTable(Tables.RollingStocks)
        //         .WithPrimaryKey(new
        //         {
        //             catalog_item_id = item.Id.ToGuid(),
        //             rolling_stock_id = rollingStock.Id.ToGuid()
        //         })
        //         .ShouldNotExists();
        // }
    }

    public static class DatabaseTestHelpersExtensions
    {
        public static void ArrangeWithOneCatalogItem(this DatabaseTestHelpers db, CatalogItem catalogItem)
        {
            db.Setup.TruncateTable(Tables.RollingStocks);
            db.Setup.TruncateTable(Tables.CatalogItems);

            var catalogItemId = catalogItem.Id.ToGuid();

            db.Arrange.Insert(Tables.CatalogItems, new
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
                db.Arrange.Insert(Tables.RollingStocks, new
                {
                    rolling_stock_id = rs.Id.ToGuid(),
                    catalog_item_id = catalogItemId,
                    category = rs.Category.ToString(),
                    epoch = rs.Epoch.ToString(),
                    railway_id = rs.Railway.Id.ToGuid()
                });
            }
        }
    }
}