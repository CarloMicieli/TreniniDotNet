using System.Collections.Immutable;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishList : IWishListInfo
    {
        string Owner { get; }

        IImmutableList<IWishlistItem> Items { get; }
    }
}
