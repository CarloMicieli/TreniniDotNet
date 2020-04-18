using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class GetWishlistsByOwner : ValidatedUseCase<GetWishlistsByOwnerInput, IGetWishlistsByOwnerOutputPort>, IGetWishlistsByOwnerUseCase
    {
        private readonly WishlistService _wishlistService;

        public GetWishlistsByOwner(IGetWishlistsByOwnerOutputPort output, WishlistService wishlistService)
            : base(new GetWishlistsByOwnerInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
        }

        protected override async Task Handle(GetWishlistsByOwnerInput input)
        {
            var owner = new Owner(input.Owner);

            var wishlists = await _wishlistService.GetByOwnerAsync(owner, input.Visibility);
            if (wishlists is null || wishlists.Count() == 0)
            {
                OutputPort.WishlistsNotFoundForTheOwner(owner, input.Visibility);
                return;
            }

            var output = new GetWishlistsByOwnerOutput(
                owner,
                input.Visibility,
                wishlists);
            OutputPort.Standard(output);
        }
    }
}
