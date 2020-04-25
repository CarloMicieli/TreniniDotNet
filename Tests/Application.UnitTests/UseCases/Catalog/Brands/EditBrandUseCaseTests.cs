using FluentAssertions;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.UseCases.Catalog.Brands
{
    public class EditBrandUseCaseTests : CatalogUseCaseTests<EditBrand, EditBrandOutput, EditBrandOutputPort>
    {
        [Fact]
        public async Task EditBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            await useCase.Execute(NewEditBrandInput.With(WebsiteUrl: "--invalid--"));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditBrand_ShouldOutputBrandNotFound_WhenBrandToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            var brandSlug = Slug.Of("ACME");

            await useCase.Execute(NewEditBrandInput.With(BrandSlug: brandSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertBrandWasNotFound(brandSlug);
        }

        [Fact]
        public async Task EditBrand_ShouldEditBrands()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeBrandsUseCase(Start.WithSeedData, NewCreateBrand);

            var brandSlug = Slug.Of("ACME");

            await useCase.Execute(NewEditBrandInput.With(BrandSlug: brandSlug, Name: "A.C.M.E."));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Slug.Should().Be(Slug.Of("A.C.M.E."));
        }

        private EditBrand NewCreateBrand(BrandService brandService, EditBrandOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditBrand(outputPort, brandService, unitOfWork);
        }
    }
}
