using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public interface IEditWishlistItemOutputPort : IOutputPortStandard<EditWishlistItemOutput>
    {
        void WishlistItemNotFound(WishlistId id, WishlistItemId itemId);
    }
}
