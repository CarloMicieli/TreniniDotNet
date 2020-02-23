using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Application.InMemory.Repositories;
using TreniniDotNet.Application.InMemory.Repositories.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetBrandBySlugTests
    {
        [Fact]
        public async Task GetBrandBySlug_ReturnError_WhenInputIsNull()
        {
            var brandRepository = new BrandRepository(new InMemoryContext());

            string output = "";

            var outputPortMock = new Mock<IGetBrandBySlugOutputPort>();
            outputPortMock.Setup(h => h.Error(It.IsAny<string>()))
                .Callback<string>(o => output = o);
            
            var useCase = NewUseCase(brandRepository, outputPortMock.Object);

            await useCase.Execute(null);

            outputPortMock.Verify(outputPort => outputPort.Error(It.IsAny<string>()), Times.Once);
            Assert.Equal("The use case input is null", output);
        }

        [Fact]
        public async Task GetBrandBySlug_ReturnInvalidRequest_WhenInputIsInvalid()
        {
            var brandRepository = new BrandRepository(new InMemoryContext());

            IList<ValidationFailure> output = null;

            var outputPortMock = new Mock<IGetBrandBySlugOutputPort>();
            outputPortMock.Setup(h => h.InvalidRequest(It.IsAny<IList<ValidationFailure>>()))
                .Callback<IList<ValidationFailure>>(o => output = o);

            var useCase = NewUseCase(brandRepository, outputPortMock.Object);

            await useCase.Execute(new GetBrandBySlugInput(Slug.Empty));

            outputPortMock.Verify(outputPort => outputPort.InvalidRequest(It.IsAny<IList<ValidationFailure>>()), Times.Once);
            Assert.True(output?.Count > 0);
        }
           
        [Fact]
        public async Task GetBrandBySlug_ReturnsTheBrandWithTheProvidedSlug()
        {
            var brandRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            GetBrandBySlugOutput output = null;
            var outputPortMock = new Mock<IGetBrandBySlugOutputPort>();
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
            var outputPortMock = new Mock<IGetBrandBySlugOutputPort>();
            outputPortMock.Setup(h => h.BrandNotFound(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewUseCase(brandRepository, outputPortMock.Object);

            var input = new GetBrandBySlugInput(Slug.Of("not-found"));
            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.BrandNotFound(It.IsAny<string>()), Times.Once);
            Assert.Equal("Brand 'not-found' not found", message);
        }

        private GetBrandBySlug NewUseCase(IBrandsRepository repo, IGetBrandBySlugOutputPort outputPort)
        {
            var brandService = new BrandService(repo);

            var useCase = new GetBrandBySlug(outputPort, brandService);
            return useCase;
        }
    }
}
