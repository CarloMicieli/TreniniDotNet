using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public class CreateCatalogItemUseCaseTests :
        CatalogItemUseCaseTests<CreateCatalogItemUseCase, CreateCatalogItemInput, CreateCatalogItemOutput, CreateCatalogItemOutputPort>
    {
        [Fact]
        public async Task CreateCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewCreateCatalogItemInput.Empty;

            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheBrandExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewCreateCatalogItemInput.With(
                brand: "not found",
                itemNumber: "12345",
                description: "My new catalog item",
                powerMethod: "dc",
                scale: "h0",
                rollingStocks: RollingStockList("III", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.AssertBrandWasNotFound(Slug.Of("not found"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheCatalogItemDoesNotExistAlready()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = NewCreateCatalogItemInput.With(
                brand: "acme",
                itemNumber: "60458",
                description: "My new catalog item",
                powerMethod: "dc",
                scale: "h0",
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemAlreadyExists(CatalogSeedData.Brands.NewAcme(), new ItemNumber("60458"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheScaleExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = NewCreateCatalogItemInput.With(
                brand: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                powerMethod: "dc",
                scale: "not exists",
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("not exists"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheRollingStockRailwayExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = NewCreateCatalogItemInput.With(
                brand: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                powerMethod: "dc",
                scale: "H0",
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "not found"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(new List<Slug>() { Slug.Of("not found") });
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckThat_AllRollingStockRailwaysExist()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = NewCreateCatalogItemInput.With(
                brand: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                powerMethod: "dc",
                scale: "H0",
                rollingStocks: RollingStockList(
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "not found1"),
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "not found2")));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(new List<Slug>() { Slug.Of("not found1"), Slug.Of("not found2") });
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCreateANewCatalogItem()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var expectedSlug = Slug.Of("acme-99999");
            var input = NewCreateCatalogItemInput.With(
                brand: "acme",
                itemNumber: "99999",
                description: "My new catalog item",
                powerMethod: "dc",
                scale: "H0",
                rollingStocks: RollingStockList(
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "fs"),
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "fs")));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var catalogItem = dbContext.CatalogItems.FirstOrDefault(it => it.Slug == expectedSlug);
            catalogItem.Should().NotBeNull();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.Slug.Should().Be(expectedSlug);
            output.Id.Should().Be(catalogItem.Id);
        }

        private IReadOnlyList<RollingStockInput> EmptyRollingStocks()
        {
            return new List<RollingStockInput>();
        }

        private static IReadOnlyList<RollingStockInput> RollingStockList(string era, string category, string railway)
        {
            var rollingStockInput = NewRollingStockInput.With(
                epoch: era,
                category: category,
                railway: railway);
            return new List<RollingStockInput>() { rollingStockInput };
        }

        private static RollingStockInput RollingStock(string era, string category, string railway)
        {
            return NewRollingStockInput.With(
                epoch: era,
                category: category,
                railway: railway,
                length: new LengthOverBufferInput(999M, null));
        }

        private static IReadOnlyList<RollingStockInput> RollingStockList(params RollingStockInput[] inputs)
        {
            return inputs.ToList();
        }

        private CreateCatalogItemUseCase CreateUseCase(
            ICreateCatalogItemOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new CreateCatalogItemUseCase(new CreateCatalogItemInputValidator(), outputPort, rollingStocksFactory, catalogItemsService, unitOfWork);
    }
}
