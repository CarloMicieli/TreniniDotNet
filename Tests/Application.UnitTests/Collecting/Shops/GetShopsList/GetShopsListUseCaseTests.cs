using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Shops.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Collecting.Shops;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
{
    public class GetShopsListUseCaseTests : CollectingUseCaseTests<GetShopsListUseCase, GetShopsListOutput, GetShopsListOutputPort>
    {
        [Fact]
        public async Task GetShopsList_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.Empty, NewGetShopsList);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetShopsList_ShouldOutputShopsList()
        {
            var (useCase, outputPort) = ArrangeShopUseCase(Start.WithSeedData, NewGetShopsList);

            var input = new GetShopsListInput(new Page(0, 10));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Shops.Should().NotBeNull();
            output.Shops.Results.Should().NotBeNull();
            output.Shops.CurrentPage.Should().Be(new Page(0, 10));
        }

        private GetShopsListUseCase NewGetShopsList(
            ShopsService shopsService,
            GetShopsListOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetShopsListUseCase(outputPort, shopsService);
        }
    }
}
