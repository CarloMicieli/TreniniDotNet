using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
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
