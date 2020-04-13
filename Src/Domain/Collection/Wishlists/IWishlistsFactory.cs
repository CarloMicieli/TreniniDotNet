namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistsFactory
    {
        IWishList NewWishlist();

        IWishlistItem NewWishlistItem();
    }
}
