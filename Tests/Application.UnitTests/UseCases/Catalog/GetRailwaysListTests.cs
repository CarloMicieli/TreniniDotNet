using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;
using TreniniDotNet.Application.InMemory.OutputPorts;
using TreniniDotNet.Application.InMemory.Repositories;
using TreniniDotNet.Application.InMemory.Repositories.Catalog;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetRailwaysListTests
    {
        [Fact]
        public async Task GetRailwaysList_ShouldReturnEmptyResult_WhenNoRailwayExists()
        {
            var railwaysRepository = new RailwayRepository(new InMemoryContext());
            var (useCase, outputPort) = SetupUseCase(railwaysRepository);

            await useCase.Execute(new GetRailwaysListInput());

            Assert.NotNull(outputPort.Output);
            Assert.True(outputPort.Output.Railways.Count == 0);
        }

        [Fact]
        public async Task GetRailwaysList_ShoulReturnTheRailwaysList()
        {
            var railwaysRepository = new RailwayRepository(InMemoryContext.WithCatalogSeedData());
            var (useCase, outputPort) = SetupUseCase(railwaysRepository);

            await useCase.Execute(new GetRailwaysListInput());

            Assert.NotNull(outputPort.Output);
            Assert.True(outputPort.Output.Railways.Count > 0);
        }

        private (GetRailwaysList, GetRailwaysListOutputPort) SetupUseCase(IRailwaysRepository repo)
        {
            var railwayService = new RailwayService(repo);
            var outputPort = new GetRailwaysListOutputPort();

            return (new GetRailwaysList(outputPort, railwayService), outputPort);
        }
    }
}
