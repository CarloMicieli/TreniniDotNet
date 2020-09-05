using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner
{
    public sealed class GetWishlistsByOwnerUseCase : AbstractUseCase<GetWishlistsByOwnerInput, GetWishlistsByOwnerOutput, IGetWishlistsByOwnerOutputPort>
    {
        private readonly WishlistsService _wishlistService;

        public GetWishlistsByOwnerUseCase(
            IUseCaseInputValidator<GetWishlistsByOwnerInput> inputValidator,
            IGetWishlistsByOwnerOutputPort outputPort,
            WishlistsService wishlistService)
            : base(inputValidator, outputPort)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
        }

        protected override async Task Handle(GetWishlistsByOwnerInput input)
        {
            var owner = new Owner(input.Owner);
            var visibility = EnumHelpers.OptionalValueFor<VisibilityCriteria>(input.Visibility) ?? VisibilityCriteria.All;

            var wishlists = await _wishlistService.GetWishlistsByOwnerAsync(owner, visibility);
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
