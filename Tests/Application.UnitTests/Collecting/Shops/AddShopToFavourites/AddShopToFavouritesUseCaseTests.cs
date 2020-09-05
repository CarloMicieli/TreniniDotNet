using System.Threading.Tasks;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Shops;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public class AddShopToFavouritesUseCaseTests : ShopUseCaseTests<AddShopToFavouritesUseCase, AddShopToFavouritesInput, AddShopToFavouritesOutput, AddShopToFavouritesOutputPort>
    {
        [Fact]
        public async Task AddShopToFavourites_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task AddShopToFavourites_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewAddShopToFavouritesInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        private AddShopToFavouritesUseCase CreateUseCase(
            IAddShopToFavouritesOutputPort outputPort,
            ShopsService shopsService,
            IUnitOfWork unitOfWork) =>
            new AddShopToFavouritesUseCase(new AddShopToFavouritesInputValidator(), outputPort, shopsService, unitOfWork);
    }
}
