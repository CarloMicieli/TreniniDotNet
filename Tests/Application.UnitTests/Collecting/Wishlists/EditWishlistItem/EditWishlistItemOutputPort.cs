using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public sealed class EditWishlistItemOutputPort : OutputPortTestHelper<EditWishlistItemOutput>, IEditWishlistItemOutputPort
    {
        private MethodInvocation<WishlistId, WishlistItemId> WishlistItemNotFoundMethod { set; get; }
        private MethodInvocation<Owner> OwnerNotAuthorizedToEditThisWishlistMethod { set; get; }

        public EditWishlistItemOutputPort()
        {
            WishlistItemNotFoundMethod = MethodInvocation<WishlistId, WishlistItemId>.NotInvoked(nameof(WishlistItemNotFound));
            OwnerNotAuthorizedToEditThisWishlistMethod =
                MethodInvocation<Owner>.NotInvoked(nameof(NotAuthorizedToEditThisWishlist));
        }

        public void WishlistItemNotFound(WishlistId id, WishlistItemId itemId)
        {
            WishlistItemNotFoundMethod = WishlistItemNotFoundMethod.Invoked(id, itemId);
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            OwnerNotAuthorizedToEditThisWishlistMethod = OwnerNotAuthorizedToEditThisWishlistMethod.Invoked(owner);
        }

        public void AssertWishlistItemNotFound(WishlistId expectedId, WishlistItemId expectedItemId) =>
            WishlistItemNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedId, expectedItemId);
    }
}
