using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;
using static TreniniDotNet.Application.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public sealed class CreateBrandUseCaseTests : CatalogUseCaseTests<CreateBrandUseCase, CreateBrandOutput, CreateBrandOutputPort>
    {
        [Fact]
        public async Task CreateBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            await useCase.Execute(NewCreateBrandInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateBrand_Should_CreateANewBrand()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            var expectedSlug = Slug.Of("acme");

            var input = NewCreateBrandInput.With(
                name: "ACME",
                companyName: "Associazione Costruzioni Modellistiche Esatte",
                websiteUrl: "http://www.acmetreni.com",
                emailAddress: "mail@acmetreni.com",
                brandType: BrandKind.Industrial.ToString(),
                address: NewAddressInput.With(
                    line1: "address line1",
                    line2: "address line2",
                    postalCode: "123456",
                    city: "city",
                    country: "DE",
                    region: "region name"
                    ));

            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();
            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.Slug.Should().Be(expectedSlug);

            dbContext.Brands.Any(it => it.Slug == expectedSlug).Should().BeTrue();
        }

        [Fact]
        public async Task CreateBrand_ShouldNotCreateANewBrand_WhenBrandAlreadyExists()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewCreateBrand);

            var name = "ACME";
            var input = NewCreateBrandInput.With(
                name: name,
                companyName: "Associazione Costruzioni Modellistiche Esatte",
                websiteUrl: "http://www.acmetreni.com",
                emailAddress: "mail@acmetreni.com",
                brandType: BrandKind.Industrial.ToString()
                );

            await useCase.Execute(input);

            outputPort.ShouldHaveBrandAlreadyExistsMessage(Slug.Of(name));
        }

        [Fact]
        public async Task CreateBrand_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewCreateBrand);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        private CreateBrandUseCase NewCreateBrand(BrandService brandService, CreateBrandOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new CreateBrandUseCase(outputPort, brandService, unitOfWork);
        }
    }
}
