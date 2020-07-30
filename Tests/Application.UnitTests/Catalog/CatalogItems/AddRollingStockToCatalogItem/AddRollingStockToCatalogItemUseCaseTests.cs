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

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public class AddRollingStockToCatalogItemUseCaseTests :
        CatalogItemUseCaseTests<AddRollingStockToCatalogItemUseCase, AddRollingStockToCatalogItemInput, AddRollingStockToCatalogItemOutput, AddRollingStockToCatalogItemOutputPort>
    {
        [Fact]
        public async Task AddRollingStockToCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewAddRollingStockToCatalogItemInput.Empty;
            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddRollingStockToCatalogItem_ShouldOutputError_WhenCatalogItemToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewAddRollingStockToCatalogItemInput.With(
                Slug.Of("not-found"),
                NewRollingStockInput.With(
                    epoch: "IV",
                    railway: "FS",
                    category: Category.ElectricLocomotive.ToString()));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemWasNotFound(Slug.Of("not-found"));
        }

        [Fact]
        public async Task AddRollingStockToCatalogItem_ShouldOutputError_WhenRailwayWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = NewAddRollingStockToCatalogItemInput.With(
                CatalogSeedData.CatalogItems.Acme_60392().Slug,
                NewRollingStockInput.With(
                    epoch: "IV",
                    railway: "not found",
                    category: Category.ElectricLocomotive.ToString()));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(Slug.Of("not-found"));
        }

        [Fact]
        public async Task AddRollingStockToCatalogItem_AddTheRollingStockToTheCatalogItem()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var catalogItem = CatalogSeedData.CatalogItems.Acme_60392();
            var input = NewAddRollingStockToCatalogItemInput.With(
                catalogItem.Slug,
                NewRollingStockInput.With(
                    epoch: "IV",
                    railway: "fs",
                    category: Category.ElectricLocomotive.ToString()));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            // var modified = dbContext.CatalogItems.FirstOrDefault(it => it.Id == catalogItem.Id);
            // modified?.RollingStocks.Should().HaveCount(2);
        }

        private AddRollingStockToCatalogItemUseCase CreateUseCase(
            IAddRollingStockToCatalogItemOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new AddRollingStockToCatalogItemUseCase(new AddRollingStockToCatalogItemInputValidator(), outputPort, rollingStocksFactory, catalogItemsService, unitOfWork);
    }
}
