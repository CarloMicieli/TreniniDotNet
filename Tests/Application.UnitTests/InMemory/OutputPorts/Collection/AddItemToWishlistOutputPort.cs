using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Collection
{
    public sealed class AddItemToWishlistOutputPort : OutputPortTestHelper<AddItemToWishlistOutput>, IAddItemToWishlistOutputPort
    {
        private MethodInvocation<Slug> CatalogItemNotFoundMethod { set; get; }
        private MethodInvocation<Owner, Slug> WishlistNotFoundMethod { set; get; }
        private MethodInvocation<Owner, Slug, Slug> CatalogItemAlreadyPresentMethod { set; get; }

        public AddItemToWishlistOutputPort()
        {
            CatalogItemNotFoundMethod = MethodInvocation<Slug>.NotInvoked(nameof(CatalogItemNotFound));
            WishlistNotFoundMethod = MethodInvocation<Owner, Slug>.NotInvoked(nameof(WishlistNotFound));
            CatalogItemAlreadyPresentMethod = MethodInvocation<Owner, Slug, Slug>.NotInvoked(nameof(CatalogItemAlreadyPresent));
        }

        public void CatalogItemAlreadyPresent(Owner owner, Slug wishlistSlug, Slug catalogItem)
        {
            CatalogItemAlreadyPresentMethod = CatalogItemAlreadyPresentMethod.Invoked(owner, wishlistSlug, catalogItem);
        }

        public void CatalogItemNotFound(Slug catalogItem)
        {
            CatalogItemNotFoundMethod = CatalogItemNotFoundMethod.Invoked(catalogItem);
        }

        public void WishlistNotFound(Owner owner, Slug wishlistSlug)
        {
            WishlistNotFoundMethod = WishlistNotFoundMethod.Invoked(owner, wishlistSlug);
        }


        public void AssertCatalogItemAlreadyPresent(Owner owner, Slug wishlistSlug, Slug catalogItem) =>
            CatalogItemAlreadyPresentMethod.ShouldBeInvokedWithTheArguments(owner, wishlistSlug, catalogItem);

        public void AssertCatalogItemNotFound(Slug expectedCatalogItem) =>
            CatalogItemNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedCatalogItem);

        public void AssertWishlistNotFound(Owner owner, Slug wishlistSlug) =>
            WishlistNotFoundMethod.ShouldBeInvokedWithTheArguments(owner, wishlistSlug);
    }
}
