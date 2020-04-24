using System.Collections.Immutable;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public interface IWishlist : IWishlistInfo, IModifiableEntity
    {
        Owner Owner { get; }

        IImmutableList<IWishlistItem> Items { get; }
    }
}
