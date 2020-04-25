using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog.Brands
{
    public class GetBrandBySlugUseCaseTests : CatalogUseCaseTests<GetBrandBySlug, GetBrandBySlugOutput, GetBrandBySlugOutputPort>
    {
        [Fact]
        public async Task GetBrandBySlug_ReturnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewGetBrandBySlug);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnInvalidRequest_WhenInputIsInvalid()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewGetBrandBySlug);

            await useCase.Execute(new GetBrandBySlugInput(Slug.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnsTheBrandWithTheProvidedSlug()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewGetBrandBySlug);

            var input = new GetBrandBySlugInput(Slug.Of("acme"));
            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;
            Assert.Equal(output?.Brand?.Slug, Slug.Of("acme"));
        }

        [Fact]
        public async Task GetBrandBySlug_WhenBrandIsNotFound_OutputBrandNotFound()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewGetBrandBySlug);

            var input = new GetBrandBySlugInput(Slug.Of("not-found"));
            await useCase.Execute(input);

            outputPort.ShouldHaveBrandNotFoundMessage("Brand 'not-found' not found");
        }

        private GetBrandBySlug NewGetBrandBySlug(BrandService brandService, GetBrandBySlugOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetBrandBySlug(outputPort, brandService);
        }
    }
}
