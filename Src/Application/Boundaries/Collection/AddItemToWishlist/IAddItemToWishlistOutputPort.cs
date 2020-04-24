using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist
{
    public interface IAddItemToWishlistOutputPort : IOutputPortStandard<AddItemToWishlistOutput>
    {
        void WishlistNotFound(WishlistId wishlistId);

        void CatalogItemNotFound(Slug catalogItem);

        void CatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, ICatalogRef catalogRef);
    }
}
