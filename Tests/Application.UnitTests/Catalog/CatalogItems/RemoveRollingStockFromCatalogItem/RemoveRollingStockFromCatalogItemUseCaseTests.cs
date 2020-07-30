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

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public class RemoveRollingStockFromCatalogItemUseCaseTests :
        CatalogItemUseCaseTests<RemoveRollingStockFromCatalogItemUseCase, RemoveRollingStockFromCatalogItemInput, RemoveRollingStockFromCatalogItemOutput, RemoveRollingStockFromCatalogItemOutputPort>
    {
        [Fact]
        public async Task RemoveRollingStockFromCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewRemoveRollingStockFromCatalogItemInput.Empty;
            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveRollingStockFromCatalogItem_ShouldOutputAnErrorWhenRollingStockToDeleteWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewRemoveRollingStockFromCatalogItemInput.With(
                Slug.Of("not found"),
                RollingStockId.NewId());
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRollingStockWasNotFound(input.CatalogItemSlug, input.RollingStockId);
        }

        [Fact]
        public async Task RemoveRollingStockFromCatalogItem_ShouldRemoveTheRollingStock()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var catalogItem = CatalogSeedData.CatalogItems.NewAcme60392();
            var rsId = catalogItem.RollingStocks.First().Id;

            var input = NewRemoveRollingStockFromCatalogItemInput.With(
                catalogItem.Slug,
                rsId);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
            outputPort.UseCaseOutput.Should().NotBeNull();

            var modifiedCatalogItem = dbContext.CatalogItems
                .First(it => it.Id == catalogItem.Id);

            modifiedCatalogItem.RollingStocks
                .All(it => it.Id != rsId)
                .Should().BeTrue();
        }

        private RemoveRollingStockFromCatalogItemUseCase CreateUseCase(
            IRemoveRollingStockFromCatalogItemOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new RemoveRollingStockFromCatalogItemUseCase(new RemoveRollingStockFromCatalogItemInputValidator(), outputPort, catalogItemsService, unitOfWork);
    }
}
