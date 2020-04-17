using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsFactory
    {
        IWishList NewWishlist(Owner owner, Slug slug, string? listName, Visibility visibility);

        IWishlistItem NewWishlistItem();
    }
}
