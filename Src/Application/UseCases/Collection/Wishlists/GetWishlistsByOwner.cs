using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
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
            var visibility = EnumHelpers.OptionalValueFor<VisibilityCriteria>(input.Visibility) ?? VisibilityCriteria.All;

            var wishlists = await _wishlistService.GetByOwnerAsync(owner, visibility);
            if (wishlists is null || wishlists.Count() == 0)
            {
                OutputPort.WishlistsNotFoundForTheOwner(owner, visibility);
                return;
            }

            var output = new GetWishlistsByOwnerOutput(
                owner,
                visibility,
                wishlists);
            OutputPort.Standard(output);
        }
    }
}
