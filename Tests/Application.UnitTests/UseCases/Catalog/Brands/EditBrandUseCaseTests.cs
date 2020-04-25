using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.UseCases.Catalog.Brands
{
    public class EditBrandUseCaseTests : CatalogUseCaseTests<EditBrand, EditBrandOutput, EditBrandOutputPort>
    {
        [Fact]
        public async Task EditBrand_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewCreateBrand);

            await useCase.Execute(NewEditBrandInput.With(WebsiteUrl: "--invalid--"));

            outputPort.ShouldHaveValidationErrors();
        }

        private EditBrand NewCreateBrand(BrandService brandService, EditBrandOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new EditBrand(outputPort, brandService, unitOfWork);
        }
    }
}
