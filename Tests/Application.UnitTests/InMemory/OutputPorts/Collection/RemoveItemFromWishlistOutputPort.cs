using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class RemoveItemFromWishlistOutputPort : OutputPortTestHelper<RemoveItemFromWishlistOutput>, IRemoveItemFromWishlistOutputPort
    {
        private MethodInvocation<WishlistId, WishlistItemId> WishlistItemNotFoundMethod { set; get; }

        public RemoveItemFromWishlistOutputPort()
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
