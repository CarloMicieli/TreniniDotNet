using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlist
{
    public sealed class EditWishlistOutputPort : OutputPortTestHelper<EditWishlistOutput>, IEditWishlistOutputPort
    {
        private MethodInvocation<WishlistId> WishlistNotFoundMethod { set; get; }
        private MethodInvocation<Owner> NotAuthorizedToEditThisWishlistMethod { set; get; }

        public EditWishlistOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<WishlistId>.NotInvoked(nameof(WishlistNotFound));
            NotAuthorizedToEditThisWishlistMethod =
                MethodInvocation<Owner>.NotInvoked(nameof(NotAuthorizedToEditThisWishlist));
        }

        public void WishlistNotFound(WishlistId id)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(id);
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            NotAuthorizedToEditThisWishlistMethod = NotAuthorizedToEditThisWishlistMethod.Invoked(owner);
        }

        public void AssertWishlistNotFound(WishlistId expectedId)
        {
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedId);
        }

        public void AssertNotAuthorizedToEditThisWishlist(Owner owner)
        {
            NotAuthorizedToEditThisWishlistMethod.ShouldBeInvokedWithTheArgument(owner);
        }
    }
}
