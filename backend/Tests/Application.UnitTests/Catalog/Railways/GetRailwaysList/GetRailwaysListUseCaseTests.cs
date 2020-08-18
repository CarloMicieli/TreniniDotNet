using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Railways;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public class GetRailwaysListUseCaseTests : RailwayUseCaseTests<GetRailwaysListUseCase, GetRailwaysListInput, GetRailwaysListOutput, GetRailwaysListOutputPort>
    {
        [Fact]
        public async Task GetRailwaysList_ShouldReturnEmptyResult_WhenNoRailwayExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetRailwaysListInput(Page.Default));

            outputPort.ShouldHaveStandardOutput();

            outputPort.UseCaseOutput.Result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetRailwaysList_ShouldReturnTheRailwaysList()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetRailwaysListInput(Page.Default));

            outputPort.ShouldHaveStandardOutput();
            outputPort.UseCaseOutput.Result.Should().HaveCount(7);
        }

        private GetRailwaysListUseCase CreateUseCase(
            GetRailwaysListOutputPort outputPort,
            RailwaysService railwaysService,
            IUnitOfWork unitOfWork) =>
            new GetRailwaysListUseCase(new GetRailwaysListInputValidator(), outputPort, railwaysService);
    }
}
