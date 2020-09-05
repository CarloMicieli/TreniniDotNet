using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById
{
    public sealed class GetWishlistByIdUseCase : AbstractUseCase<GetWishlistByIdInput, GetWishlistByIdOutput, IGetWishlistByIdOutputPort>
    {
        private readonly WishlistsService _wishlistService;

        public GetWishlistByIdUseCase(
            IUseCaseInputValidator<GetWishlistByIdInput> inputValidator,
            IGetWishlistByIdOutputPort outputPort,
            WishlistsService wishlistService)
            : base(inputValidator, outputPort)
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

            if (owner.CanView(wishlist) == false)
            {
                OutputPort.WishlistNotVisible(id, wishlist.Visibility);
                return;
            }

            OutputPort.Standard(new GetWishlistByIdOutput(wishlist));
        }
    }
}
