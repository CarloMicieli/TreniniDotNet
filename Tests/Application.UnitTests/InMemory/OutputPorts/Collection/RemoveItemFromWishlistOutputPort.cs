using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class RemoveItemFromWishlistOutputPort : OutputPortTestHelper<RemoveItemFromWishlistOutput>, IRemoveItemFromWishlistOutputPort
    {
        private MethodInvocation<Owner, Slug> WishlistNotFoundMethod { set; get; }
        private MethodInvocation<Owner, Slug, WishlistItemId> WishlistItemNotFoundMethod { set; get; }

        public RemoveItemFromWishlistOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<Owner, Slug>.NotInvoked(nameof(WishlistNotFound));
            WishlistItemNotFoundMethod = MethodInvocation<Owner, Slug, WishlistItemId>.NotInvoked(nameof(WishlistItemNotFound));
        }

        public void WishlistItemNotFound(Owner owner, Slug wishlistSlug, WishlistItemId itemId)
        {
            WishlistItemNotFoundMethod = WishlistItemNotFoundMethod.Invoked(owner, wishlistSlug, itemId);
        }

        public void WishlistNotFound(Owner owner, Slug wishlistSlug)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(owner, wishlistSlug);
        }

        public void AssertWishlistNotFound(Owner owner, Slug wishlistSlug) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArguments(owner, wishlistSlug);

        public void AssertWishlistItemNotFound(Owner owner, Slug wishlistSlug, WishlistItemId itemId) =>
            WishlistItemNotFoundMethod.ShouldBeInvokedWithTheArguments(owner, wishlistSlug, itemId);
    }
}
