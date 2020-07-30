using System.Collections.Generic;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistsFactory : AggregateRootFactory<WishlistId, Wishlist>
    {
        public WishlistsFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public Wishlist CreateWishlist(
            Owner owner,
            string? listName,
            Visibility visibility,
            Budget? budget)
        {
            return new Wishlist(
                NewId(id => new WishlistId(id)),
                owner,
                listName,
                visibility,
                budget,
                new List<WishlistItem>(),
                GetCurrentInstant(),
                null,
                1);
        }
    }
}
