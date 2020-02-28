using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UnitTests.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateCatalogItemUseCaseTests : UseCaseTestHelper<CreateCatalogItem, CreateCatalogItemOutput, CreateCatalogItemOutputPort>
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
                rollingStocks: RollingStockList("III", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.BrandNameNotFoundMethod.ShouldBeInvokedWithTheArgument("The brand with name 'not found' was not found");
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
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);
           
            outputPort.ShouldHaveNoValidationError();
            outputPort.CatalogItemAlreadyExistsMethod.ShouldBeInvokedWithTheArgument("The catalog item '60458' for 'acme' already exists");
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheScaleExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme", 
                itemNumber: "99999",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "not exists",
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);
           
            outputPort.ShouldHaveNoValidationError();
            outputPort.ScaleNotFoundMethod.ShouldBeInvokedWithTheArgument("The scale 'not exists' was not found");
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheRollingStockRailwayExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme", 
                itemNumber: "99999",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "not found"));

            await useCase.Execute(input);
           
            outputPort.ShouldHaveNoValidationError();
            outputPort.RailwayNotFoundMethod.ShouldBeInvokedWithTheArguments("Any of the railway was not found", new List<string>() { "not found" });
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckThat_AllRollingStockRailwaysExist()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brandName: "acme", 
                itemNumber: "99999",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                rollingStocks: RollingStockList(
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "not found1"),
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "not found2")));

            await useCase.Execute(input);
           
            outputPort.ShouldHaveNoValidationError();
            outputPort.RailwayNotFoundMethod.ShouldBeInvokedWithTheArguments("Any of the railway was not found", new List<string>() { "not found1", "not found2" });
        }

        private CreateCatalogItem NewCreateCatalogItem(CatalogItemService catalogItemService, CreateCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateCatalogItem(outputPort, catalogItemService, unitOfWork);
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
                    length: null);
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
                    length: null);
        }

        private IList<RollingStockInput> RollingStockList(params RollingStockInput[] inputs)
        {
            return inputs.ToList();
        }
    }
}