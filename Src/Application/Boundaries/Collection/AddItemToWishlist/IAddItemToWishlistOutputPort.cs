using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public interface IAddItemToWishlistOutputPort : IOutputPortStandard<AddItemToWishlistOutput>
    {
        void WishlistNotFound(Owner owner, Slug wishlistSlug);

        void CatalogItemNotFound(Slug catalogItem);

        void CatalogItemAlreadyPresent(Owner owner, Slug wishlistSlug, Slug catalogItem);
    }
}
