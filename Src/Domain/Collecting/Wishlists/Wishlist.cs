using System;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class Wishlist : AggregateRoot<WishlistId>, IWishlist
    {
        internal Wishlist(
            WishlistId wishlistId,
            Owner owner,
            Slug slug, string? listName,
            Visibility visibility,
            IImmutableList<IWishlistItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
            : base(wishlistId, createdDate, modifiedDate, version)
        {
            Owner = owner;
            Items = items;
            Slug = slug;
            ListName = listName;
            Visibility = visibility;
        }

        public Owner Owner { get; }

        public IImmutableList<IWishlistItem> Items { get; }

        public Slug Slug { get; }

        public string? ListName { get; }

        public Visibility Visibility { get; }

        public override string ToString() => $"{Id} {Owner} {ListName}";
    }
}
