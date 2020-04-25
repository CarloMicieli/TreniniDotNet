using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UnitTests.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog.CatalogItems
{
    public class CreateCatalogItemUseCaseTests : CatalogUseCaseTests<CreateCatalogItem, CreateCatalogItemOutput, CreateCatalogItemOutputPort>
    {
        [Fact]
        public async Task CreateCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "",
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "not valid",
                scale: null,
                deliveryDate: null, available: false,
                rollingStocks: EmptyRollingStocks());

            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheBrandExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "not found",
                itemNumber: "12345",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("III", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.AssertBrandWasNotFound(Slug.Of("not found"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheCatalogItemDoesNotExistAlready()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme",
                itemNumber: "60458",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemAlreadyExists(CatalogSeedData.Brands.Acme(), new ItemNumber("60458"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheScaleExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "not exists",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("not exists"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheRollingStockRailwayExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "not found"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(new List<Slug>() { Slug.Of("not found") });
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckThat_AllRollingStockRailwaysExist()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                deliveryDate: null, available: false,
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
            var (useCase, outputPort, unitOfWork) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme",
                itemNumber: "99999",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList(
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "fs"),
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "fs")));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            Assert.Equal(Slug.Of("acme-99999"), outputPort.UseCaseOutput.Slug);
            Assert.NotEqual(CatalogItemId.Empty, outputPort.UseCaseOutput.Id);
        }

        private CreateCatalogItem NewCreateCatalogItem(CatalogItemService catalogItemService, CreateCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            ICatalogItemsFactory catalogItemsFactory = new CatalogItemsFactory(
                new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0)),
                FakeGuidSource.NewSource(new Guid("3d02506b-8263-4e14-880d-3f3caf22c562")));

            return new CreateCatalogItem(outputPort,
                catalogItemService, catalogItemsFactory,
                unitOfWork);
        }

        private IList<RollingStockInput> EmptyRollingStocks()
        {
            return new List<RollingStockInput>();
        }

        private IList<RollingStockInput> RollingStockList(string era, string category, string railway)
        {
            var rollingStockInput = new RollingStockInput(
                    era: era,
                    category: category,
                    railway: railway,
                    className: null,
                    roadNumber: null,
                    typeName: null,
                    length: null,
                    control: null,
                    dccInterface: null);
            return new List<RollingStockInput>() { rollingStockInput };
        }

        private RollingStockInput RollingStock(string era, string category, string railway)
        {
            return new RollingStockInput(
                    era: era,
                    category: category,
                    railway: railway,
                    className: null,
                    roadNumber: null,
                    typeName: null,
                    length: new LengthOverBufferInput(999M, null),
                    control: null,
                    dccInterface: null);
        }

        private IList<RollingStockInput> RollingStockList(params RollingStockInput[] inputs)
        {
            return inputs.ToList();
        }
    }
}