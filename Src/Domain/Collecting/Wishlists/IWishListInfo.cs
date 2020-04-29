using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlistInfo
    {
        WishlistId WishlistId { get; }

        Slug Slug { get; }

        string? ListName { get; }

        Visibility Visibility { get; }
    }
}
