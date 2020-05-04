using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListUseCaseTests : CatalogUseCaseTests<GetBrandsListUseCase, GetBrandsListOutput, GetBrandsListOutputPort>
    {
        [Fact]
        public async Task GetBrandsList_ShouldReturnEmptyResult_WhenNoBrandIsFound()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.Empty, NewGetBrandsList);

            await useCase.Execute(new GetBrandsListInput(new Page(0, 10)));

            var output = outputPort.UseCaseOutput;
            Assert.True(output.Result.Count() == 0);
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnBrandsList_WithPagination()
        {
            var expextedElements = 2;
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewGetBrandsList);

            await useCase.Execute(new GetBrandsListInput(new Page(0, expextedElements)));

            var output = outputPort.UseCaseOutput;
            Assert.Equal(expextedElements, output.Result.Count());
        }

        private GetBrandsListUseCase NewGetBrandsList(BrandService brandService, GetBrandsListOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetBrandsListUseCase(outputPort, brandService);
        }
    }
}
