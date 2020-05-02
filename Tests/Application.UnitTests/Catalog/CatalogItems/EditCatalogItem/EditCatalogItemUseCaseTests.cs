using System.Threading.Tasks;
using TreniniDotNet.Application.InMemory.Catalog.CatalogItems.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

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
                itemSlug: Slug.Of("acme-60392"),
                description: "Modified description");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
        }


        private EditCatalogItemUseCase NewEditCatalogItem(CatalogItemService catalogItemService, EditCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditCatalogItemUseCase(outputPort, catalogItemService, unitOfWork);
        }
    }
}
