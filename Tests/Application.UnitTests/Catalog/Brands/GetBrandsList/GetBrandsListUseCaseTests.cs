using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public class GetBrandsListUseCaseTests : BrandUseCaseTests<GetBrandsListUseCase, GetBrandsListInput, GetBrandsListOutput, GetBrandsListOutputPort>
    {
        [Fact]
        public async Task GetBrandsList_ShouldReturnEmptyResult_WhenNoBrandIsFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetBrandsListInput(new Page(0, 10)));

            var output = outputPort.UseCaseOutput;

            output.Result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturnBrandsList_WithPagination()
        {
            var expectedElements = 2;
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetBrandsListInput(new Page(0, expectedElements)));

            var output = outputPort.UseCaseOutput;

            output.Result.Should().HaveCount(expectedElements);
        }

        private GetBrandsListUseCase CreateUseCase(
            GetBrandsListOutputPort outputPort,
            BrandsService brandService,
            IUnitOfWork unitOfWork) =>
            new GetBrandsListUseCase(new GetBrandsListInputValidator(), outputPort, brandService);
    }
}
