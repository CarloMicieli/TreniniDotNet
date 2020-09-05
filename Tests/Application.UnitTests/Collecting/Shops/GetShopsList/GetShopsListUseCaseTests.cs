using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shops;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
{
    public class GetShopsListUseCaseTests : ShopUseCaseTests<GetShopsListUseCase, GetShopsListInput, GetShopsListOutput, GetShopsListOutputPort>
    {
        [Fact]
        public async Task GetShopsList_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetShopsList_ShouldOutputShopsList()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = new GetShopsListInput(new Page(0, 10));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.PaginatedResult.Should().NotBeNull();
            output.PaginatedResult.Results.Should().NotBeNull();
            output.PaginatedResult.CurrentPage.Should().Be(new Page(0, 10));
        }

        private GetShopsListUseCase CreateUseCase(
            IGetShopsListOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new GetShopsListUseCase(new GetShopsListInputValidator(), outputPort, shopsService);
    }
}
