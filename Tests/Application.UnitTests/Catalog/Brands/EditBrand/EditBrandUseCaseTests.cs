using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public class EditBrandUseCaseTests : CatalogUseCaseTests<EditBrandUseCase, EditBrandOutput, EditBrandOutputPort>
    {
        [Fact]
        public async Task EditBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            await useCase.Execute(NewEditBrandInput.With(websiteUrl: "--invalid--"));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditBrand_ShouldOutputBrandNotFound_WhenBrandToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            var brandSlug = Slug.Of("ACME");

            await useCase.Execute(NewEditBrandInput.With(brandSlug: brandSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertBrandWasNotFound(brandSlug);
        }

        [Fact]
        public async Task EditBrand_ShouldEditBrands()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeBrandsUseCase(Start.WithSeedData, NewCreateBrand);

            var brandSlug = Slug.Of("ACME");

            await useCase.Execute(NewEditBrandInput.With(brandSlug: brandSlug, name: "A.C.M.E."));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Slug.Should().Be(Slug.Of("A.C.M.E."));
        }

        private EditBrandUseCase NewCreateBrand(BrandService brandService, EditBrandOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditBrandUseCase(outputPort, brandService, unitOfWork);
        }
    }
}
