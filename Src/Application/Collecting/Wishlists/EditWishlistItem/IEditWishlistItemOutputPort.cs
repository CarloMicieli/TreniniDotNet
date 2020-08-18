using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public interface IEditWishlistItemOutputPort : IStandardOutputPort<EditWishlistItemOutput>
    {
        void WishlistItemNotFound(WishlistId id, WishlistItemId itemId);

        void NotAuthorizedToEditThisWishlist(Owner owner);
    }
}
