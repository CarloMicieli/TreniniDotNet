using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class AddItemToWishlistOutputPort : OutputPortTestHelper<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        private MethodInvocation<Slug> CatalogItemNotFoundMethod { set; get; }
        private MethodInvocation<WishlistId> WishlistNotFoundMethod { set; get; }
        private MethodInvocation<WishlistId, WishlistItemId, ICatalogRef> CatalogItemAlreadyPresentMethod { set; get; }

        public AddItemToWishlistOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<WishlistId>.NotInvoked(nameof(WishlistNotFound));
            CatalogItemNotFoundMethod = MethodInvocation<Slug>.NotInvoked(nameof(CatalogItemNotFound));
            CatalogItemAlreadyPresentMethod = MethodInvocation<WishlistId, WishlistItemId, ICatalogRef>.NotInvoked(nameof(CatalogItemAlreadyPresent));
        }

        public void CatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, ICatalogRef catalogRef)
        {
            CatalogItemAlreadyPresentMethod = CatalogItemAlreadyPresentMethod.Invoked(wishlistId, itemId, catalogRef);
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            CatalogItemNotFoundMethod = CatalogItemNotFoundMethod.Invoked(catalogItem);
        }

        public void WishlistNotFound(WishlistId wishlistId)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(wishlistId);
        }

        public void AssertCatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, ICatalogRef catalogRef) =>
            CatalogItemAlreadyPresentMethod.ShouldBeInvokedWithTheArguments(wishlistId, itemId, catalogRef);

        public void AssertCatalogItemNotFound(Slug expectedCatalogItem) =>
            CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedCatalogItem);

        public void AssertWishlistNotFound(WishlistId expectedWishlistId) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedWishlistId);
    }
}
