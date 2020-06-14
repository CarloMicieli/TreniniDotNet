using System;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public class AddRollingStockToCatalogItemUseCaseTests : CatalogUseCaseTests<AddRollingStockToCatalogItemUseCase, AddRollingStockToCatalogItemOutput, AddRollingStockToCatalogItemOutputPort>
    {
        [Fact]
        public async Task AddRollingStockToCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewAddRollingStockToCatalogItem);

            var input = CatalogInputs.NewAddRollingStockToCatalogItemInput.Empty;
            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddRollingStockToCatalogItem_ShouldOutputError_WhenCatalogItemToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewAddRollingStockToCatalogItem);

            var input = CatalogInputs.NewAddRollingStockToCatalogItemInput.With(
                Slug.Of("not-found"),
                CatalogInputs.NewRollingStockInput.With(
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
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewAddRollingStockToCatalogItem);

            var input = CatalogInputs.NewAddRollingStockToCatalogItemInput.With(
                CatalogSeedData.CatalogItems.Acme_60392().Slug,
                CatalogInputs.NewRollingStockInput.With(
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
            var (useCase, outputPort, unitOfWork) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewAddRollingStockToCatalogItem);

            var input = CatalogInputs.NewAddRollingStockToCatalogItemInput.With(
                CatalogSeedData.CatalogItems.Acme_60392().Slug,
                CatalogInputs.NewRollingStockInput.With(
                    epoch: "IV",
                    railway: "fs",
                    category: Category.ElectricLocomotive.ToString()));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
        }

        private AddRollingStockToCatalogItemUseCase NewAddRollingStockToCatalogItem(CatalogItemService catalogItemService, AddRollingStockToCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            var fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));

            ICatalogItemsFactory catalogItemsFactory = new CatalogItemsFactory(
                fakeClock,
                FakeGuidSource.NewSource(new Guid("3d02506b-8263-4e14-880d-3f3caf22c562")));

            IRollingStocksFactory rollingStocksFactory = new RollingStocksFactory(
                fakeClock, FakeGuidSource.NewSource(Guid.NewGuid()));

            return new AddRollingStockToCatalogItemUseCase(outputPort,
                catalogItemService, rollingStocksFactory,
                unitOfWork);
        }

    }
}
