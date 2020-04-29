using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public interface IAddItemToWishlistOutputPort : IOutputPortStandard<AddItemToWishlistOutput>
    {
        void WishlistNotFound(WishlistId wishlistId);

        void CatalogItemNotFound(Slug catalogItem);

        void CatalogItemAlreadyPresent(WishlistId wishlistId, WishlistItemId itemId, ICatalogRef catalogRef);
    }
}
