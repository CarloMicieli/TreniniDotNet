using Xunit;
using Moq;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Application.Boundaries.CreateBrand;
using TreniniDotNet.Application.InMemory;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Application.SeedData.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Application.UseCases.Catalog;

namespace TreniniDotNet.Application.UseCases
{
    public class CreateBrandTests
    {
        [Fact]
        public async Task CreateBrand_Should_CreateANewBrand()
        {
            var brandRepository = new BrandRepository(new InMemoryContext());

            CreateBrandOutput output = null;
            var outputPortMock = new Mock<IOutputPort>();
            outputPortMock.Setup(h => h.Standard(It.IsAny<CreateBrandOutput>()))
                .Callback<CreateBrandOutput>(o => output = o);

            var useCase = NewCreateBrandUseCase(brandRepository, outputPortMock.Object);

            var input = new CreateBrandInput(
                "ACME",
                "Associazione Costruzioni Modellistiche Esatte",
                new Uri("http://www.acmetreni.com"),
                new MailAddress("mail@acmetreni.com"),
                BrandKind.Industrial
                );

            await useCase.Execute(input);

            Assert.NotNull(output);
            Assert.True(output!.BrandId != null);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("acme"), output!.Slug);

            Assert.NotNull(brandRepository.GetBy(output!.BrandId));
        }

        [Fact]
        public async Task CreateBrand_ShouldNotCreateANewBrand_WhenBrandAlreadyExists()
        {
            var brandRepository = new BrandRepository(InMemoryContext.WithCatalogSeedData());

            string message = "";
            var outputPortMock = new Mock<IOutputPort>();
            outputPortMock.Setup(h => h.BrandAlreadyExists(It.IsAny<string>()))
                .Callback<string>(msg => message = msg);

            var useCase = NewCreateBrandUseCase(brandRepository, outputPortMock.Object);

            var input = new CreateBrandInput(
                "ACME",
                "Associazione Costruzioni Modellistiche Esatte",
                new Uri("http://www.acmetreni.com"),
                new MailAddress("mail@acmetreni.com"),
                BrandKind.Industrial
                );

            await useCase.Execute(input);

            outputPortMock.Verify(outputPort => outputPort.BrandAlreadyExists(It.IsAny<string>()), Times.Once);
            Assert.Equal("Brand 'ACME' already exists", message);
        }
        
        private CreateBrand NewCreateBrandUseCase(IBrandsRepository repo, IOutputPort outputPort)
        {
            var brandFactory = new DomainBrandFactory();
            var brandService = new BrandService(repo, brandFactory);

            var useCase = new CreateBrand(brandService, outputPort, new UnitOfWork());
            return useCase;
        }
    }
}
