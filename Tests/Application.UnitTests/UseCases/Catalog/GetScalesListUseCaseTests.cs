using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;
using TreniniDotNet.Application.InMemory.OutputPorts.Catalog;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Pagination;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetScalesListUseCaseTests : UseCaseTestHelper<GetScalesList, GetScalesListOutput, GetScalesListOutputPort>
    {
        [Fact]
        public async Task GetScalesList_ShouldReturnEmptyResult_WhenNoBrandIsFound()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.Empty, NewGetScalesList);

            await useCase.Execute(new GetScalesListInput(Page.Default));

            var output = outputPort.UseCaseOutput;
            Assert.True(output.Result.Count() == 0);
        }

        [Fact]
        public async Task GetScalesList_ShouldReturnBrandsList()
        {
            var (useCase, outputPort) = ArrangeScalesUseCase(Start.WithSeedData, NewGetScalesList);

            await useCase.Execute(new GetScalesListInput(Page.Default));

            var output = outputPort.UseCaseOutput;
            Assert.True(output.Result.Count() > 0);
        }

        private GetScalesList NewGetScalesList(ScaleService scaleService, GetScalesListOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetScalesList(outputPort, scaleService);
        }
    }
}
