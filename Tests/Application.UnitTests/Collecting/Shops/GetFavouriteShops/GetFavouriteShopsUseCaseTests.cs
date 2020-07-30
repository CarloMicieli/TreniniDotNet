using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
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

        private GetFavouriteShopsUseCase CreateUseCase(
            IGetFavouriteShopsOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new GetFavouriteShopsUseCase(new GetFavouriteShopsInputValidator(), outputPort);
    }
}
