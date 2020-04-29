using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistUseCase : ValidatedUseCase<RemoveItemFromWishlistInput, IRemoveItemFromWishlistOutputPort>, IRemoveItemFromWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromWishlistUseCase(IRemoveItemFromWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new RemoveItemFromWishlistInputValidator(), output)
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

            var exists = await _wishlistService.ExistAsync(owner, id);
            if (exists == false)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            var item = await _wishlistService.GetItemByIdAsync(id, itemId);
            if (item is null)
            {
                OutputPort.WishlistItemNotFound(id, itemId);
                return;
            }

            await _wishlistService.DeleteItemAsync(id, itemId);
            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveItemFromWishlistOutput(id, itemId));
        }
    }
}
