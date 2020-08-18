using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public sealed class EditBrandUseCaseTests : BrandUseCaseTests<EditBrandUseCase, EditBrandInput, EditBrandOutput, EditBrandOutputPort>
    {
        [Fact]
        public async Task EditBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditBrandInput.With(websiteUrl: "--invalid--"));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditBrand_ShouldOutputBrandNotFound_WhenBrandToEditWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var brandSlug = Slug.Of("ACME");

            await useCase.Execute(NewEditBrandInput.With(brandSlug: brandSlug));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertBrandWasNotFound(brandSlug);
        }

        [Fact]
        public async Task EditBrand_ShouldUpdateBrands()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var brandSlug = Slug.Of("ACME");
            var expectedSlug = Slug.Of("A.C.M.E.");

            await useCase.Execute(NewEditBrandInput.With(brandSlug: brandSlug, name: "A.C.M.E."));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Slug.Should().Be(expectedSlug);

            dbContext.Brands.Any(it => it.Slug == brandSlug).Should().BeFalse();
            dbContext.Brands.Any(it => it.Slug == expectedSlug).Should().BeTrue();
        }

        private EditBrandUseCase CreateUseCase(
            EditBrandOutputPort outputPort,
            BrandsService brandService,
            IUnitOfWork unitOfWork) =>
            new EditBrandUseCase(new EditBrandInputValidator(), outputPort, brandService, unitOfWork);
    }
}
