using FluentAssertions;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetShopsList;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Pagination;
using Xunit;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public class GetShopsListUseCaseTests : CollectionUseCaseTests<GetShopsList, GetShopsListOutput, GetShopsListOutputPort>
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

        private GetShopsList NewGetShopsList(
            ShopsService shopsService,
            GetShopsListOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetShopsList(outputPort, shopsService);
        }
    }
}
