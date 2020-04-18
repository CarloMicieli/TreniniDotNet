using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist
{
    public interface IRemoveItemFromWishlistOutputPort : IOutputPortStandard<RemoveItemFromWishlistOutput>
    {
        void WishlistItemNotFound(WishlistId id, WishlistItemId itemId);
    }
}
