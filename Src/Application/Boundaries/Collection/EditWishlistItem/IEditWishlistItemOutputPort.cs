using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem
{
    public interface IEditWishlistItemOutputPort : IOutputPortStandard<EditWishlistItemOutput>
    {
        void WishlistNotFound(Owner owner, Slug wishlistSlug);

        void WishlistItemNotFound(Owner owner, Slug wishlistSlug, WishlistItemId itemId);
    }
}
