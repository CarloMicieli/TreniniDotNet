using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems
{
    public class CatalogItemsRepositoryTests : DapperRepositoryUnitTests<ICatalogItemsRepository>
    {
        public CatalogItemsRepositoryTests()
            : base(unitOfWork => new CatalogItemsRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task CatalogItemsRepository_AddAsync_ShouldCreateNewCatalogItems()
        {
            Database.ArrangeWithoutCatalogItems();

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
            var Acme = CatalogSeedData.Brands.Acme;
            var item = CatalogSeedData.CatalogItems.Acme60392;
            Database.ArrangeWithOneCatalogItem(item);

            var exists = await Repository.ExistsAsync(Acme, item.ItemNumber);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task CatalogItemsRepository_ExistsAsync_ShouldReturnFalseWhenCatalogItemDoesNotExist()
        {
            var Acme = CatalogSeedData.Brands.Acme;
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

        [Fact]
        public async Task CatalogItemRepository_UpdateAsync_ShouldUpdateCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.NewAcme60392();
            Database.ArrangeWithOneCatalogItem(item);

            var modified = item.With(
                description: "Modified description",
                prototypeDescription: "Modified prototype description",
                modelDescription: "Modified model description",
                deliveryDate: DeliveryDate.FirstQuarterOf(2020));

            await Repository.UpdateAsync(modified);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.CatalogItems)
                .WithPrimaryKey(new
                {
                    catalog_item_id = item.Id.ToGuid()
                })
                .AndValues(new
                {
                    description = modified.Description,
                    model_description = modified.ModelDescription,
                    prototype_description = modified.PrototypeDescription,
                    delivery_date = "2020/Q1"
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CatalogItemRepository_UpdateAsync_ShouldAddNewRollingStocks()
        {
            var id = Guid.NewGuid();
            var rollingStocksFactory = new RollingStocksFactory(
                FakeGuidSource.NewSource(id));

            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            Database.ArrangeWithOneCatalogItem(catalogItem);

            var newLocomotive = rollingStocksFactory.CreateLocomotive(
                new RailwayRef(CatalogSeedData.Railways.Fs),
                Category.ElectricLocomotive,
                Epoch.IV,
                LengthOverBuffer.OfMillimeters(210M),
                MinRadius.OfMillimeters(360M),
                Prototype.OfLocomotive("E652", "057"),
                Couplers.Nem352,
                "blu grigio",
                "Milano C.le",
                DccInterface.Mtc21,
                Control.DccReady);
            catalogItem.AddRollingStock(newLocomotive);

            await Repository.UpdateAsync(catalogItem);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.RollingStocks)
                .WithPrimaryKey(new
                {
                    catalog_item_id = catalogItem.Id.ToGuid(),
                    rolling_stock_id = newLocomotive.Id.ToGuid()
                })
                .AndValues(new
                {
                    railway_id = newLocomotive.Railway.Id.ToGuid(),
                    category = newLocomotive.Category.ToString(),
                    epoch = newLocomotive.Epoch.ToString(),
                    length_mm = 210M,
                    min_radius = 360M,
                    road_number = newLocomotive.Prototype?.RoadNumber,
                    class_name = newLocomotive.Prototype?.ClassName,
                    livery = newLocomotive.Livery,
                    depot = newLocomotive.Depot,
                    dcc_interface = newLocomotive.DccInterface.ToString(),
                    control = newLocomotive.Control.ToString()
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CatalogItemRepository_UpdateAsync_ShouldUpdateRollingStocks()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            Database.ArrangeWithOneCatalogItem(catalogItem);

            var locomotive = catalogItem.RollingStocks.First() as Locomotive;
            var modified = locomotive.With(
                epoch: Epoch.IV,
                length: LengthOverBuffer.OfMillimeters(210M),
                minRadius: MinRadius.OfMillimeters(360M),
                prototype: Prototype.OfLocomotive("E652", "057"),
                couplers: Couplers.Nem352,
                livery: "blu grigio",
                depot: "Milano C.le",
                dccInterface: DccInterface.Mtc21,
                control: Control.DccReady);
            catalogItem.UpdateRollingStock(modified);

            await Repository.UpdateAsync(catalogItem);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.RollingStocks)
                .WithPrimaryKey(new
                {
                    catalog_item_id = catalogItem.Id.ToGuid(),
                    rolling_stock_id = modified.Id.ToGuid()
                })
                .AndValues(new
                {
                    railway_id = modified.Railway.Id.ToGuid(),
                    category = modified.Category.ToString(),
                    epoch = modified.Epoch.ToString(),
                    length_mm = 210M,
                    min_radius = 360M,
                    road_number = modified.Prototype?.RoadNumber,
                    class_name = modified.Prototype?.ClassName,
                    livery = modified.Livery,
                    depot = modified.Depot,
                    dcc_interface = modified.DccInterface.ToString(),
                    control = modified.Control.ToString()
                })
                .ShouldExists();
        }

        [Fact]
        public async Task CatalogItemRepository_UpdateAsync_ShouldRemoveRollingStocks()
        {
            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            Database.ArrangeWithOneCatalogItem(catalogItem);

            var rollingStockId = catalogItem.RollingStocks.First().Id;
            catalogItem.RemoveRollingStock(rollingStockId);

            await Repository.UpdateAsync(catalogItem);
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.RollingStocks)
                .WithPrimaryKey(new
                {
                    catalog_item_id = catalogItem.Id.ToGuid(),
                    rolling_stock_id = rollingStockId.ToGuid()
                })
                .AndValues(new
                { })
                .ShouldNotExists();
        }
    }
}