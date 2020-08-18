using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public sealed class CreateBrandUseCaseTests : BrandUseCaseTests<CreateBrandUseCase, CreateBrandInput, CreateBrandOutput, CreateBrandOutputPort>
    {
        [Fact]
        public async Task CreateBrand_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewCreateBrandInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateBrand_Should_CreateANewBrand()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.Empty, CreateUseCase);

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
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

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

        private CreateBrandUseCase CreateUseCase(
            CreateBrandOutputPort outputPort,
            BrandsService brandService,
            IUnitOfWork unitOfWork) =>
            new CreateBrandUseCase(new CreateBrandInputValidator(), outputPort, brandService, unitOfWork);
    }
}
