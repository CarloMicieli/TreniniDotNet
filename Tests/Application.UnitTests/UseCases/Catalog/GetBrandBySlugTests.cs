using Moq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.GetBrandBySlug;
using TreniniDotNet.Application.InMemory;
using TreniniDotNet.Application.InMemory.Catalog;
using TreniniDotNet.Application.SeedData.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetBrandBySlugTests
    {
        [Fact]
        public async Task GetBrandBySlug_ReturnsTheBrandWithTheProvidedSlug()
        {
            var brandRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            GetBrandBySlugOutput output = null;
            var outputPortMock = new Mock<IOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<GetBrandBySlugOutput>()))
                .Callback<GetBrandBySlugOutput>(o => output = o);

            var useCase = NewUseCase(brandRepository, outputPortMock.Object);

            var input = new GetBrandBySlugInput(Slug.Of("acme"));
            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.Standard(It.IsAny<GetBrandBySlugOutput>()), Times.Once);
            Assert.Equal(output?.Brand?.Slug, Slug.Of("acme"));
        }

        [Fact]
        public async Task GetBrandBySlug_WhenBrandIsNotFound_OutputBrandNotFound()
        {
            var brandRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<IOutputPort>();
            outputPortMock.Setup(h => h.BrandNotFound(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewUseCase(brandRepository, outputPortMock.Object);

            var input = new GetBrandBySlugInput(Slug.Of("not-found"));
            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.BrandNotFound(It.IsAny<string>()), Times.Once);
            Assert.Equal("not found", message);
        }

        private GetBrandBySlug NewUseCase(IBrandsRepository repo, IOutputPort outputPort)
        {
            var brandFactory = new DomainBrandFactory();
            var brandService = new BrandService(repo, brandFactory);

            var useCase = new GetBrandBySlug(outputPort, brandService);
            return useCase;
        }
    }
}
