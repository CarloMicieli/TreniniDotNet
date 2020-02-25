using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Pagination;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetBrandsListUseCaseTests : UseCaseTestHelper<GetBrandsList, GetBrandsListOutput, GetBrandsListOutputPort>
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
        public async Task GetBrandsList_ShouldReturnBrandsList_WithoutPagination()
        {
            var (useCase, outputPort) = ArrangeBrandsUseCase(Start.WithSeedData, NewGetBrandsList);

            await useCase.Execute(new GetBrandsListInput());

            var output = outputPort.UseCaseOutput;
            Assert.True(output.Result.Count() > 0);
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

        private GetBrandsList NewGetBrandsList(BrandService brandService, GetBrandsListOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetBrandsList(outputPort, brandService);
        }

    }
}
