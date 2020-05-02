using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Scales;
using Xunit;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public class GetScalesListUseCaseTests : CatalogUseCaseTests<GetScalesListUseCase, GetScalesListOutput, GetScalesListOutputPort>
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

        private GetScalesListUseCase NewGetScalesList(ScaleService scaleService, GetScalesListOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            return new GetScalesListUseCase(outputPort, scaleService);
        }
    }
}
