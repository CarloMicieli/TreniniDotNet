using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsByOwner;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class GetWishlistsByOwnerOutputPort : OutputPortTestHelper<GetWishlistsByOwnerOutput>, IGetWishlistsByOwnerOutputPort
    {
        private MethodInvocation<Owner, Visibility> WishlistsNotFoundForTheOwnerMethod { set; get; }

        public GetWishlistsByOwnerOutputPort()
        {
            WishlistsNotFoundForTheOwnerMethod = MethodInvocation<Owner, Visibility>.NotInvoked(nameof(WishlistsNotFoundForTheOwner));
        }

        public void WishlistsNotFoundForTheOwner(Owner owner, Visibility visibility)
        {
            WishlistsNotFoundForTheOwnerMethod = WishlistsNotFoundForTheOwnerMethod.Invoked(owner, visibility);
        }

        public void AssertWishlistsNotFoundForTheOwner(Owner expectedOwner, Visibility expectedVisibility) =>
            WishlistsNotFoundForTheOwnerMethod.ShouldBeInvokedWithTheArguments(expectedOwner, expectedVisibility);

    }
}
