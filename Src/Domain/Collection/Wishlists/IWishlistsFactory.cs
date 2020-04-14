using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsFactory
    {
        IWishList NewWishlist(string owner, Visibility visibility, string? listName);

        IWishlistItem NewWishlistItem();
    }
}
