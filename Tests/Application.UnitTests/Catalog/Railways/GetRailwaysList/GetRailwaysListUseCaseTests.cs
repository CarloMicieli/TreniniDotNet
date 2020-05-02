using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public sealed class GetRailwaysListUseCaseTests : CatalogUseCaseTests<GetRailwaysListUseCase, GetRailwaysListOutput, GetRailwaysListOutputPort>
    {
        [Fact]
        public async Task GetRailwaysList_ShouldReturnEmptyResult_WhenNoRailwayExists()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.Empty, NewGetRailwaysList);

            await useCase.Execute(new GetRailwaysListInput(Page.Default));

            outputPort.ShouldHaveStandardOutput();
            Assert.True(outputPort.UseCaseOutput.Result.Count() == 0);
        }

        [Fact]
        public async Task GetRailwaysList_ShoulReturnTheRailwaysList()
        {
            var (useCase, outputPort) = ArrangeRailwaysUseCase(Start.WithSeedData, NewGetRailwaysList);

            await useCase.Execute(new GetRailwaysListInput(Page.Default));

            outputPort.ShouldHaveStandardOutput();
            Assert.True(outputPort.UseCaseOutput.Result.Count() > 0);
        }

        private GetRailwaysListUseCase NewGetRailwaysList(RailwayService railwayService, GetRailwaysListOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetRailwaysListUseCase(outputPort, railwayService);
        }
    }
}
