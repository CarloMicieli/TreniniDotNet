using Xunit;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateBrandUseCaseTests : UseCaseTestHelper<CreateBrand, CreateBrandOutput, CreateBrandOutputPort>
    {
        [Fact]
        public async Task CreateBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            await useCase.Execute(new CreateBrandInput(null, null, null, null, null));

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateBrand_Should_CreateANewBrand()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            var input = new CreateBrandInput(
                "ACME",
                "Associazione Costruzioni Modellistiche Esatte",
                "http://www.acmetreni.com",
                "mail@acmetreni.com",
                BrandKind.Industrial.ToString()
                );

            await useCase.Execute(input);

            outputPort.ShouldHaveStandardOutput();

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
            var input = new CreateBrandInput(
                name,
                "Associazione Costruzioni Modellistiche Esatte",
                "http://www.acmetreni.com",
                "mail@acmetreni.com",
                BrandKind.Industrial.ToString()
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
