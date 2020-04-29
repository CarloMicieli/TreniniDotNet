using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public interface IRemoveItemFromWishlistOutputPort : IOutputPortStandard<RemoveItemFromWishlistOutput>
    {
        void WishlistItemNotFound(WishlistId id, WishlistItemId itemId);
    }
}
