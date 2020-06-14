using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public interface IWishlist : IWishlistInfo
    {
        Owner Owner { get; }

        IImmutableList<IWishlistItem> Items { get; }

        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }
    }
}
