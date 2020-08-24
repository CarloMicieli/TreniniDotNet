using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public class GetFavouriteShopsUseCaseTests : ShopUseCaseTests<GetFavouriteShopsUseCase, GetFavouriteShopsInput, GetFavouriteShopsOutput, GetFavouriteShopsOutputPort>
    {
        [Fact]
        public async Task GetFavouriteShops_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetFavouriteShops_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewGetFavouriteShopsInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task GetFavouriteShops_ShouldReturnTheFavouriteShops()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(NewGetFavouriteShopsInput.With("George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var result = outputPort.UseCaseOutput.FavouriteShops;
            result.Should().HaveCount(1);

            outputPort.UseCaseOutput.Owner.Should().Be(new Owner("George"));
        }

        private GetFavouriteShopsUseCase CreateUseCase(
            IGetFavouriteShopsOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new GetFavouriteShopsUseCase(new GetFavouriteShopsInputValidator(), outputPort, shopsService);
    }
}
