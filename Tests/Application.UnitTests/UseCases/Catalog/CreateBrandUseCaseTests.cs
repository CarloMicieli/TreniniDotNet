using Xunit;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateBrandUseCaseTests : CatalogUseCaseTests<CreateBrand, CreateBrandOutput, CreateBrandOutputPort>
    {
        [Fact]
        public async Task CreateBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            await useCase.Execute(NewBrandInput.Empty());

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateBrand_Should_CreateANewBrand()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            var input = NewBrandInput.With(
                Name: "ACME",
                CompanyName: "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl: "http://www.acmetreni.com",
                EmailAddress: "mail@acmetreni.com",
                BrandType: BrandKind.Industrial.ToString(),
                Address: NewAddressInput.With(
                    Line1: "address line1",
                    Line2: "address line2",
                    PostalCode: "123456",
                    City: "city",
                    Country: "DE",
                    Region: "region name"
                    ));

            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();
            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            Assert.NotNull(output);
            Assert.True(output!.Slug != null);
            Assert.Equal(Slug.Of("acme"), output!.Slug);
        }

        [Fact]
        public async Task CreateBrand_ShouldNotCreateANewBrand_WhenBrandAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewCreateBrand);

            var name = "ACME";
            var input = NewBrandInput.With(
                Name: name,
                CompanyName: "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl: "http://www.acmetreni.com",
                EmailAddress: "mail@acmetreni.com",
                BrandType: BrandKind.Industrial.ToString()
                );

            await useCase.Execute(input);

            outputPort.ShouldHaveBrandAlreadyExistsMessage($"Brand '{name}' already exists");
        }

        [Fact]
        public async Task CreateBrand_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewCreateBrand);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        private CreateBrand NewCreateBrand(BrandService brandService, CreateBrandOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateBrand(outputPort, brandService, unitOfWork);
        }
    }
}
