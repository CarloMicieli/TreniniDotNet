using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts
{
    public sealed class GetWishlistsByOwnerOutputPort : OutputPortTestHelper<GetWishlistsByOwnerOutput>, IGetWishlistsByOwnerOutputPort
    {
        private MethodInvocation<Owner, VisibilityCriteria> WishlistsNotFoundForTheOwnerMethod { set; get; }

        public GetWishlistsByOwnerOutputPort()
        {
            WishlistsNotFoundForTheOwnerMethod = MethodInvocation<Owner, VisibilityCriteria>.NotInvoked(nameof(WishlistsNotFoundForTheOwner));
        }

        public void WishlistsNotFoundForTheOwner(Owner owner, VisibilityCriteria visibility)
        {
            WishlistsNotFoundForTheOwnerMethod = WishlistsNotFoundForTheOwnerMethod.Invoked(owner, visibility);
        }

        public void AssertWishlistsNotFoundForTheOwner(Owner expectedOwner, VisibilityCriteria expectedVisibility) =>
            WishlistsNotFoundForTheOwnerMethod.ShouldBeInvokedWithTheArguments(expectedOwner, expectedVisibility);

    }
}
