using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem
{
    public interface IEditWishlistItemOutputPort : IOutputPortStandard<EditWishlistItemOutput>
    {
        void WishlistItemNotFound(WishlistId id, WishlistItemId itemId);
    }
}
