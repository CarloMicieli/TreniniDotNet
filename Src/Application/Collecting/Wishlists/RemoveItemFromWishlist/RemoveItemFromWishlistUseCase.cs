using System;
using System.Threading.Tasks;
using NodaTime;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistUseCase : AbstractUseCase<RemoveItemFromWishlistInput, RemoveItemFromWishlistOutput, IRemoveItemFromWishlistOutputPort>
    {
        private readonly WishlistsService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromWishlistUseCase(
            IUseCaseInputValidator<RemoveItemFromWishlistInput> inputValidator,
            IRemoveItemFromWishlistOutputPort outputPort,
            WishlistsService wishlistService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(RemoveItemFromWishlistInput input)
        {
            var id = new WishlistId(input.Id);
            var itemId = new WishlistItemId(input.ItemId);
            var owner = new Owner(input.Owner);

            var wishlist = await _wishlistService.GetByIdAsync(id);
            if (wishlist is null)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            var item = wishlist.FindItemById(itemId);
            if (item is null)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            wishlist.RemoveItem(item.Id, LocalDate.MaxIsoValue);

            await _wishlistService.UpdateWishlistAsync(wishlist);
            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveItemFromWishlistOutput(id, itemId));
        }
    }
}
