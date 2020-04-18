using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class GetWishlistById : ValidatedUseCase<GetWishlistByIdInput, IGetWishlistByIdOutputPort>, IGetWishlistByIdUseCase
    {
        private readonly WishlistService _wishlistService;

        public GetWishlistById(IGetWishlistByIdOutputPort output, WishlistService wishlistService)
            : base(new GetWishlistByIdInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
        }

        protected override async Task Handle(GetWishlistByIdInput input)
        {
            var id = new WishlistId(input.Id);

            var wishlist = await _wishlistService.GetByIdAsync(id);
            if (wishlist is null)
            {
                OutputPort.WishlistNotFound(id);
                return;
            }

            OutputPort.Standard(new GetWishlistByIdOutput(wishlist));
        }
    }
}
