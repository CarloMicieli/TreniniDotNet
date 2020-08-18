using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistOutputPort : OutputPortTestHelper<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        private MethodInvocation<Slug> CatalogItemNotFoundMethod { set; get; }
        private MethodInvocation<WishlistId> WishlistNotFoundMethod { set; get; }
        private MethodInvocation<WishlistId, CatalogItemRef> CatalogItemAlreadyPresentMethod { set; get; }

        public AddItemToWishlistOutputPort()
        {
            WishlistNotFoundMethod = MethodInvocation<WishlistId>.NotInvoked(nameof(WishlistNotFound));
            CatalogItemNotFoundMethod = MethodInvocation<Slug>.NotInvoked(nameof(CatalogItemNotFound));
            CatalogItemAlreadyPresentMethod = MethodInvocation<WishlistId, CatalogItemRef>.NotInvoked(nameof(CatalogItemAlreadyPresent));
        }

        public void CatalogItemAlreadyPresent(WishlistId wishlistId, CatalogItemRef catalogRef)
        {
            CatalogItemAlreadyPresentMethod = CatalogItemAlreadyPresentMethod.Invoked(wishlistId, catalogRef);
        }

        public void NotAuthorizedToEditThisWishlist(Owner owner)
        {
            throw new System.NotImplementedException();
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            CatalogItemNotFoundMethod = CatalogItemNotFoundMethod.Invoked(catalogItem);
        }

        public void WishlistNotFound(WishlistId wishlistId)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(wishlistId);
        }

        public void AssertCatalogItemAlreadyPresent(WishlistId wishlistId, CatalogItemRef catalogRef) =>
            CatalogItemAlreadyPresentMethod.ShouldBeInvokedWithTheArguments(wishlistId, catalogRef);

        public void AssertCatalogItemNotFound(Slug expectedCatalogItem) =>
            CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedCatalogItem);

        public void AssertWishlistNotFound(WishlistId expectedWishlistId) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedWishlistId);
    }
}
