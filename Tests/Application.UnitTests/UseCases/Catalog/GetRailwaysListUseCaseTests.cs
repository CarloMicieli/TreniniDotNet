using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Pagination;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetRailwaysListUseCaseTests : CatalogUseCaseTests<GetRailwaysList, GetRailwaysListOutput, GetRailwaysListOutputPort>
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

        private GetRailwaysList NewGetRailwaysList(RailwayService railwayService, GetRailwaysListOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetRailwaysList(outputPort, railwayService);
        }
    }
}
