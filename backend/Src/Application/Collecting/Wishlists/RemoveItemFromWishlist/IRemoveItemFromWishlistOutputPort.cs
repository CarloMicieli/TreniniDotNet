using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public interface IRemoveItemFromWishlistOutputPort : IStandardOutputPort<RemoveItemFromWishlistOutput>
    {
        void WishlistItemNotFound(WishlistId id, WishlistItemId itemId);
    }
}
