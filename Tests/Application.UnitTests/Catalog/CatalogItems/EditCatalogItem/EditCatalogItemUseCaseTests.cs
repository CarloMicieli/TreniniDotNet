using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public class EditCatalogItemUseCaseTests : CatalogUseCaseTests<EditCatalogItemUseCase, EditCatalogItemOutput, EditCatalogItemOutputPort>
    {
        [Fact]
        public async Task EditCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewEditCatalogItem);

            await useCase.Execute(NewEditCatalogItemInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputCatalogItemNotFound_WhenSlugToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewEditCatalogItem);

            await useCase.Execute(NewEditCatalogItemInput.With(itemSlug: Slug.Of("acme-99999")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemWasNotFound(Slug.Of("acme-99999"));
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputBrandNotFound_WhenBrandWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                itemSlug: Slug.Of("acme-60392"),
                brand: "--not found--");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertBrandWasNotFound(Slug.Of("--not found--"));
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputScaleNotFound_WhenScaleWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                itemSlug: Slug.Of("acme-60392"),
                scale: "not found");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("--not found--"));
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputRailwayNotFound_WhenARailwayWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                itemSlug: Slug.Of("acme-60392"),
                rollingStocks: RollingStockList(Epoch.IV.ToString(), Category.DieselLocomotive.ToString(), "--not found--"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(new List<Slug>() { Slug.Of("--not found--") });
        }

        [Fact]
        public async Task EditCatalogItem_ShouldUpdateCatalogItem()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                itemSlug: Slug.Of("acme-60392"),
                description: "Modified description");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var catalogItem = dbContext.CatalogItems.FirstOrDefault(it => it.Slug == input.Slug);
            catalogItem.Should().NotBeNull();

            catalogItem?.Description.Should().Be(input.Values.Description);
        }

        private EditCatalogItemUseCase NewEditCatalogItem(CatalogItemService catalogItemService, EditCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            var fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));

            IRollingStocksFactory rollingStocksFactory = new RollingStocksFactory(
                fakeClock, FakeGuidSource.NewSource(Guid.NewGuid()));

            return new EditCatalogItemUseCase(outputPort, rollingStocksFactory, catalogItemService, unitOfWork);
        }

        private static IReadOnlyList<RollingStockInput> RollingStockList(string era, string category, string railway)
        {
            var rollingStockInput = NewRollingStockInput.With(
                epoch: era,
                category: category,
                railway: railway);
            return new List<RollingStockInput>() { rollingStockInput };
        }
    }
}
