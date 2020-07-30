using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public sealed class RemoveShopFromFavouritesUseCase : AbstractUseCase<RemoveShopFromFavouritesInput, RemoveShopFromFavouritesOutput, IRemoveShopFromFavouritesOutputPort>
    {
        private readonly ShopsService _shopsService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveShopFromFavouritesUseCase(
            IUseCaseInputValidator<RemoveShopFromFavouritesInput> inputValidator,
            IRemoveShopFromFavouritesOutputPort output,
            ShopsService shopsService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _shopsService = shopsService ?? throw new ArgumentNullException(nameof(shopsService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(RemoveShopFromFavouritesInput input)
        {
            var shopId = new ShopId(input.ShopId);
            bool exists = await _shopsService.ExistsAsync(shopId);
            if (!exists)
            {
                OutputPort.ShopNotFound(shopId);
                return;
            }

            var owner = new Owner(input.Owner);

            await _shopsService.RemoveFromFavourites(owner, shopId);
            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveShopFromFavouritesOutput());
        }
    }
}
