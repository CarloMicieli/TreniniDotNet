using Xunit;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.UseCases.Catalog.CatalogItems
{
    public class EditCatalogItemUseCaseTests : CatalogUseCaseTests<EditCatalogItem, EditCatalogItemOutput, EditCatalogItemOutputPort>
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

            await useCase.Execute(NewEditCatalogItemInput.With(ItemSlug: Slug.Of("acme-99999")));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemWasNotFound(Slug.Of("acme-99999"));
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputBrandNotFound_WhenBrandWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                ItemSlug: Slug.Of("acme-60392"),
                Brand: "--not found--");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertBrandWasNotFound(Slug.Of("--not found--"));
        }

        [Fact]
        public async Task EditCatalogItem_ShouldOutputScaleNotFound_WhenScaleWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                ItemSlug: Slug.Of("acme-60392"),
                Scale: "not found");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("--not found--"));
        }

        //[Fact]
        //public async Task EditCatalogItem_ShouldOutputRailwayNotFound_WhenARailwayWasNotFound()
        //{
        //    var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

        //    var input = NewEditCatalogItemInput.With(
        //        ItemSlug: Slug.Of("acme-60392"),
        //        RollingStocks: ListOf());

        //    await useCase.Execute(input);

        //    outputPort.ShouldHaveNoValidationError();
        //    outputPort.AssertScaleWasNotFound(Slug.Of("--not found--"));
        //}

        [Fact]
        public async Task EditCatalogItem_ShouldUpdateCatalogItem()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewEditCatalogItem);

            var input = NewEditCatalogItemInput.With(
                ItemSlug: Slug.Of("acme-60392"),
                Description: "Modified description");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
        }


        private EditCatalogItem NewEditCatalogItem(CatalogItemService catalogItemService, EditCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditCatalogItem(outputPort, catalogItemService, unitOfWork);
        }
    }
}
