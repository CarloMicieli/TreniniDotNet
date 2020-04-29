using System.Collections.Immutable;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlist : IWishlistInfo, IModifiableEntity
    {
        Owner Owner { get; }

        IImmutableList<IWishlistItem> Items { get; }
    }
}
