using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdUseCase : ValidatedUseCase<GetWishlistByIdInput, IGetWishlistByIdOutputPort>, IGetWishlistByIdUseCase
    {
        private readonly WishlistService _wishlistService;

        public GetWishlistByIdUseCase(IGetWishlistByIdOutputPort output, WishlistService wishlistService)
            : base(new GetWishlistByIdInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
        }

        protected override async Task Handle(GetWishlistByIdInput input)
        {
            var id = new WishlistId(input.Id);
            var owner = new Owner(input.Owner);

            var wishlist = await _wishlistService.GetByIdAsync(id);
            if (wishlist is null)
            {
                OutputPort.WishlistNotFound(id);
                return;
            }

            if (wishlist.Owner == owner ||
                wishlist.Visibility == Visibility.Public)
            {
                OutputPort.Standard(new GetWishlistByIdOutput(wishlist));
            }
            else
            {
                OutputPort.WishlistNotVisible(id, wishlist.Visibility);
            }
        }
    }
}
