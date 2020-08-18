using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public class EditCatalogItemUseCaseTests :
        CatalogItemUseCaseTests<EditCatalogItemUseCase, EditCatalogItemInput, EditCatalogItemOutput, EditCatalogItemOutputPort>
    {
        [Fact]
        public async Task EditCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditCatalogItemInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputCatalogItemNotFound_WhenSlugToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditCatalogItemInput.With(itemSlug: Slug.Of("acme-99999")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemWasNotFound(Slug.Of("acme-99999"));
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputBrandNotFound_WhenBrandWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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

        private static IReadOnlyList<RollingStockInput> RollingStockList(string epoch, string category, string railway)
        {
            var rollingStockInput = NewRollingStockInput.With(
                epoch: epoch,
                category: category,
                railway: railway);
            return new List<RollingStockInput>() { rollingStockInput };
        }

        private EditCatalogItemUseCase CreateUseCase(
            IEditCatalogItemOutputPort outputPort,
            CatalogItemsService catalogItemsService,
            RollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork) =>
            new EditCatalogItemUseCase(new EditCatalogItemInputValidator(), outputPort, rollingStocksFactory, catalogItemsService, unitOfWork);
    }
}
