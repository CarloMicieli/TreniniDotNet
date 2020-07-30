using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public sealed class AddShopToFavouritesUseCase : AbstractUseCase<AddShopToFavouritesInput, AddShopToFavouritesOutput, IAddShopToFavouritesOutputPort>
    {
        private readonly ShopsService _shopsService;
        private readonly IUnitOfWork _unitOfWork;

        public AddShopToFavouritesUseCase(
            IUseCaseInputValidator<AddShopToFavouritesInput> inputValidator,
            IAddShopToFavouritesOutputPort output,
            ShopsService shopsService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _shopsService = shopsService ?? throw new ArgumentNullException(nameof(shopsService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(AddShopToFavouritesInput input)
        {
            var shopId = new ShopId(input.ShopId);
            var shopExists = await _shopsService.ExistsAsync(shopId);
            if (!shopExists)
            {
                OutputPort.ShopNotFound(shopId);
                return;
            }

            var owner = new Owner(input.Owner);

            await _shopsService.AddShopToFavouritesAsync(owner, shopId);
            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new AddShopToFavouritesOutput());
        }
    }
}
