using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerUseCase : ValidatedUseCase<GetWishlistsByOwnerInput, IGetWishlistsByOwnerOutputPort>, IGetWishlistsByOwnerUseCase
    {
        private readonly WishlistService _wishlistService;

        public GetWishlistsByOwnerUseCase(IGetWishlistsByOwnerOutputPort output, WishlistService wishlistService)
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
