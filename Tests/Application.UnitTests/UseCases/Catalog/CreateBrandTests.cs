using Xunit;
using Moq;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.InMemory;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Application.InMemory.Catalog;
using System.Collections.Generic;
using FluentValidation.Results;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateBrandTests
    {
        [Fact]
        public async Task CreateBrand_ShouldValidateInput()
        {
            var brandRepository = new BrandRepository(new InMemoryContext());

            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
            var outputPortMock = new Mock<ICreateBrandOutputPort>();
            outputPortMock.Setup(h => h.InvalidRequest(It.IsAny<IList<ValidationFailure>>()))
                .Callback<IList<ValidationFailure>>(o => validationFailures = o);

            var useCase = NewCreateBrandUseCase(brandRepository, outputPortMock.Object);

            await useCase.Execute(new CreateBrandInput(null, null, null, null, null));

            outputPortMock.Verify(outputPort => outputPort.InvalidRequest(It.IsAny<List<ValidationFailure>>()), Times.Once);
            Assert.True(validationFailures.Count > 0);
        }

        [Fact]
        public async Task CreateBrand_Should_CreateANewBrand()
        {
            var brandRepository = new BrandRepository(new InMemoryContext());

            CreateBrandOutput output = null;
            var outputPortMock = new Mock<ICreateBrandOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<CreateBrandOutput>()))
                .Callback<CreateBrandOutput>(o => output = o);

            var useCase = NewCreateBrandUseCase(brandRepository, outputPortMock.Object);

            var input = new CreateBrandInput(
                "ACME",
                "Associazione Costruzioni Modellistiche Esatte",
                "http://www.acmetreni.com",
                "mail@acmetreni.com",
                BrandKind.Industrial.ToString()
                );

            await useCase.Execute(input);

            Assert.NotNull(output);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("acme"), output!.Slug);

            Assert.NotNull(brandRepository.GetBy(output!.Slug));
        }

        [Fact]
        public async Task CreateBrand_ShouldNotCreateANewBrand_WhenBrandAlreadyExists()
        {
            var brandRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<ICreateBrandOutputPort>();
            outputPortMock.Setup(h => h.BrandAlreadyExists(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateBrandUseCase(brandRepository, outputPortMock.Object);

            var input = new CreateBrandInput(
                "ACME",
                "Associazione Costruzioni Modellistiche Esatte",
                "http://www.acmetreni.com",
                "mail@acmetreni.com",
                BrandKind.Industrial.ToString()
                );

            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.BrandAlreadyExists(It.IsAny<string>()), Times.Once);
            Assert.Equal("Brand 'ACME' already exists", message);
        }

        [Fact]
        public async Task CreateBrand_ShouldOutputAnError_WhenInputIsNull()
        {
            var brandRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<ICreateBrandOutputPort>();
            outputPortMock.Setup(h => h.Error(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateBrandUseCase(brandRepository, outputPortMock.Object);

            await useCase.Execute(null);

            outputPortMock.Verify(outputPort => outputPort.Error(It.IsAny<string>()), Times.Once);
            Assert.Equal("The use case input is null", message);
        }

        private CreateBrand NewCreateBrandUseCase(IBrandsRepository repo, ICreateBrandOutputPort outputPort)
        {
            var brandService = new BrandService(repo);

            var useCase = new CreateBrand(outputPort, brandService, new UnitOfWork());
            return useCase;
        }
    }
}
