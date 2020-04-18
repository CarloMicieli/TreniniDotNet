using TreniniDotNet.Common;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlistInfo : IModifiableEntity
    {
        WishlistId WishlistId { get; }

        Slug Slug { get; }

        string? ListName { get; }

        Visibility Visibility { get; }
    }
}
