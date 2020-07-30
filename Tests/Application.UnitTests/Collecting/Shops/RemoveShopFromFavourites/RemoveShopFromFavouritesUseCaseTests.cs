using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shops;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public class RemoveShopFromFavouritesUseCaseTests : ShopUseCaseTests<RemoveShopFromFavouritesUseCase, RemoveShopFromFavouritesInput, RemoveShopFromFavouritesOutput, RemoveShopFromFavouritesOutputPort>
    {
        [Fact]
        public async Task RemoveShopFromFavourites_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task RemoveShopFromFavourites_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewRemoveShopFromFavouritesInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        private RemoveShopFromFavouritesUseCase CreateUseCase(
            IRemoveShopFromFavouritesOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new RemoveShopFromFavouritesUseCase(new RemoveShopFromFavouritesInputValidator(), outputPort, shopsService, unitOfWork);
    }
}
