using System;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishList : IWishlist, IEquatable<WishList>
    {
        internal WishList(WishlistId wishlistId,
            Owner owner,
            Slug slug, string? listName,
            Visibility visibility,
            IImmutableList<IWishlistItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
        {
            Owner = owner;
            Items = items;
            WishlistId = wishlistId;
            Slug = slug;
            ListName = listName;
            Visibility = visibility;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Version = version;
        }

        public Owner Owner { get; }

        public IImmutableList<IWishlistItem> Items { get; }

        public WishlistId WishlistId { get; }

        public Slug Slug { get; }

        public string? ListName { get; }

        public Visibility Visibility { get; }

        public Instant CreatedDate { get; }

        public Instant? ModifiedDate { get; }

        public int Version { get; }

        #region [ Equality ]

        public static bool operator ==(WishList left, WishList right) => AreEquals(left, right);

        public static bool operator !=(WishList left, WishList right) => !AreEquals(left, right);


        public override bool Equals(object obj)
        {
            if (obj is WishList that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(WishList other) => AreEquals(this, other);

        private static bool AreEquals(WishList left, WishList right) =>
            left.WishlistId == right.WishlistId &&
            left.Owner == right.Owner;

        #endregion

        public override int GetHashCode() => HashCode.Combine(WishlistId, Owner);

        public override string ToString() => $"{WishlistId} {Owner} {ListName}";
    }
}