using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public class GetScalesListUseCaseTests : ScaleUseCaseTests<GetScalesListUseCase, GetScalesListInput, GetScalesListOutput, GetScalesListOutputPort>
    {
        [Fact]
        public async Task GetScalesList_ShouldReturnEmptyResult_WhenNoBrandIsFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetScalesListInput(Page.Default));

            var output = outputPort.UseCaseOutput;
            output.Result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetScalesList_ShouldReturnBrandsList()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(new GetScalesListInput(Page.Default));

            var output = outputPort.UseCaseOutput;
            output.Result.Should().HaveCount(8);
        }

        private GetScalesListUseCase CreateUseCase(
            GetScalesListOutputPort outputPort,
            ScalesService scalesService,
            IUnitOfWork unitOfWork) =>
            new GetScalesListUseCase(new GetScalesListInputValidator(), outputPort, scalesService);
    }
}
