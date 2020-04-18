using System.Collections.Immutable;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlist : IWishlistInfo
    {
        Owner Owner { get; }

        IImmutableList<IWishlistItem> Items { get; }
    }
}
