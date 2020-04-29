using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts
{
    public sealed class EditWishlistItemOutputPort : OutputPortTestHelper<EditWishlistItemOutput>, IEditWishlistItemOutputPort
    {
        private MethodInvocation<WishlistId, WishlistItemId> WishlistItemNotFoundMethod { set; get; }

        public EditWishlistItemOutputPort()
        {
            WishlistItemNotFoundMethod = MethodInvocation<WishlistId, WishlistItemId>.NotInvoked(nameof(WishlistItemNotFound));
        }

        public void WishlistItemNotFound(WishlistId id, WishlistItemId itemId)
        {
            WishlistItemNotFoundMethod = WishlistItemNotFoundMethod.Invoked(id, itemId);
        }

        public void AssertWishlistItemNotFound(WishlistId expectedId, WishlistItemId expectedItemId) =>
            WishlistItemNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedId, expectedItemId);
    }
}
