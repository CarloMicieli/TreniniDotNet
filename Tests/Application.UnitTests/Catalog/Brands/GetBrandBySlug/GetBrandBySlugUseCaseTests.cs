using System.Threading.Tasks;
using TreniniDotNet.Application.InMemory.Catalog.Brands.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public class GetBrandBySlugUseCaseTests : CatalogUseCaseTests<GetBrandBySlugUseCase, GetBrandBySlugOutput, GetBrandBySlugOutputPort>
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

        private GetBrandBySlugUseCase NewGetBrandBySlug(BrandService brandService, GetBrandBySlugOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetBrandBySlugUseCase(outputPort, brandService);
        }
    }
}
