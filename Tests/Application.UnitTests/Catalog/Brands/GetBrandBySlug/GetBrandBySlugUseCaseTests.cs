using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public sealed class GetBrandBySlugUseCaseTests : BrandUseCaseTests<GetBrandBySlugUseCase, GetBrandBySlugInput, GetBrandBySlugOutput, GetBrandBySlugOutputPort>
    {
        [Fact]
        public async Task GetBrandBySlug_ReturnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnInvalidRequest_WhenInputIsInvalid()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetBrandBySlugInput(Slug.Empty));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnsTheBrandWithTheProvidedSlug()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = new GetBrandBySlugInput(Slug.Of("acme"));
            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();
            var output = outputPort.UseCaseOutput;
            Assert.Equal(output?.Brand?.Slug, Slug.Of("acme"));
        }

        [Fact]
        public async Task GetBrandBySlug_WhenBrandIsNotFound_OutputBrandNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = new GetBrandBySlugInput(Slug.Of("not-found"));
            await useCase.Execute(input);

            outputPort.ShouldHaveBrandNotFoundMessage("Brand 'not-found' not found");
        }

        private GetBrandBySlugUseCase CreateUseCase(
            GetBrandBySlugOutputPort outputPort,
            BrandsService brandService,
            IUnitOfWork unitOfWork) =>
            new GetBrandBySlugUseCase(new GetBrandBySlugInputValidator(), outputPort, brandService);
    }
}
