using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public class EditRollingStockUseCaseTests : CatalogUseCaseTests<EditRollingStockUseCase, EditRollingStockOutput, EditRollingStockOutputPort>
    {
        [Fact]
        public async Task EditRollingStock_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewEditRollingStock);

            var input = CatalogInputs.NewEditRollingStockInput.Empty;
            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditRollingStock_ShouldOutputErrorWhenCatalogItemToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewEditRollingStock);

            var input = CatalogInputs.NewEditRollingStockInput.With(
                Slug.Of("not-found"),
                RollingStockId.NewId());
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemWasNotFound(input.Slug);
        }

        [Fact]
        public async Task EditRollingStock_ShouldOutputErrorWhenRollingStockToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditRollingStock);

            var catalogItem = CatalogSeedData.CatalogItems.Acme_60392();

            var input = CatalogInputs.NewEditRollingStockInput.With(
                catalogItem.Slug,
                RollingStockId.NewId());
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRollingStockWasNotFound(input.Slug, input.RollingStockId);
        }

        [Fact]
        public async Task EditRollingStock_ShouldOutputErrorWhenRailwayWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditRollingStock);

            var catalogItem = CatalogSeedData.CatalogItems.Acme_60392();

            var input = CatalogInputs.NewEditRollingStockInput.With(
                catalogItem.Slug,
                catalogItem.RollingStocks.First().RollingStockId,
                railway: "Not found");
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(Slug.Of("Not found"));
        }

        [Fact]
        public async Task EditRollingStock_ShouldEditRollingStocks()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditRollingStock);

            var catalogItem = CatalogSeedData.CatalogItems.Acme_60392();
            var rsId = catalogItem.RollingStocks.First().RollingStockId;

            var input = CatalogInputs.NewEditRollingStockInput.With(
                catalogItem.Slug,
                rsId,
                roadNumber: "Modified");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            outputPort.UseCaseOutput.Should().NotBeNull();

            var modifiedCatalogItem = dbContext.CatalogItems
                .First(it => it.CatalogItemId == catalogItem.CatalogItemId);

            modifiedCatalogItem.RollingStocks
                .Any(it => it.RollingStockId == rsId && it.Prototype!.RoadNumber == "Modified")
                .Should().BeTrue();
        }

        private EditRollingStockUseCase NewEditRollingStock(CatalogItemService catalogItemService, EditRollingStockOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            var fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));

            IRollingStocksFactory rollingStocksFactory = new RollingStocksFactory(
                fakeClock, FakeGuidSource.NewSource(Guid.NewGuid()));

            return new EditRollingStockUseCase(outputPort,
                catalogItemService, rollingStocksFactory,
                unitOfWork);
        }
    }
}
