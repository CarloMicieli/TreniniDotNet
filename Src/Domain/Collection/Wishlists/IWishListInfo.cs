using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistInfo
    {
        WishlistId WishlistId { get; }

        Slug Slug { get; }

        string? ListName { get; }

        Visibility Visibility { get; }
    }
}
