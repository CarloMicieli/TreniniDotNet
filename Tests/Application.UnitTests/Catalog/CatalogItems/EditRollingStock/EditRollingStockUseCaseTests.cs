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

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public class EditRollingStockUseCaseTests :
        CatalogItemUseCaseTests<EditRollingStockUseCase, EditRollingStockInput, EditRollingStockOutput, EditRollingStockOutputPort>
    {
        [Fact]
        public async Task EditRollingStock_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewEditRollingStockInput.Empty;
            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditRollingStock_ShouldOutputErrorWhenCatalogItemToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewEditRollingStockInput.With(
                Slug.Of("not-found"),
                RollingStockId.NewId());
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemWasNotFound(input.Slug);
        }

        [Fact]
        public async Task EditRollingStock_ShouldOutputErrorWhenRollingStockToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();

            var input = NewEditRollingStockInput.With(
                catalogItem.Slug,
                RollingStockId.NewId());
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRollingStockWasNotFound(input.Slug, input.RollingStockId);
        }

        [Fact]
        public async Task EditRollingStock_ShouldOutputErrorWhenRailwayWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();

            var input = NewEditRollingStockInput.With(
                catalogItem.Slug,
                catalogItem.RollingStocks.First().Id,
                railway: "Not found");
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(Slug.Of("Not found"));
        }

        [Fact]
        public async Task EditRollingStock_ShouldEditRollingStocks()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            var rsId = catalogItem.RollingStocks.First().Id;

            var input = NewEditRollingStockInput.With(
                catalogItem.Slug,
                rsId,
                roadNumber: "Modified");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            outputPort.UseCaseOutput.Should().NotBeNull();

            var modifiedCatalogItem = dbContext.CatalogItems
                .First(it => it.Id == catalogItem.Id);
        }

        private EditRollingStockUseCase CreateUseCase(
            IEditRollingStockOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new EditRollingStockUseCase(new EditRollingStockInputValidator(), outputPort, rollingStocksFactory, catalogItemsService, unitOfWork);
    }
}
