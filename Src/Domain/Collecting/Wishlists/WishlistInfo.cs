using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistInfo : IWishlistInfo, IEquatable<WishlistInfo>
    {
        public WishlistInfo(WishlistId wishlistId, Slug slug, string? listName, Visibility visibility)
        {
            Id = wishlistId;
            Slug = slug;
            ListName = listName;
            Visibility = visibility;
        }

        public WishlistId Id { get; }

        public Slug Slug { get; }

        public string? ListName { get; }

        public Visibility Visibility { get; }

        #region [ Equality ]

        public bool Equals(WishlistInfo other) =>
            this.Id == other.Id;

        public override bool Equals(object obj)
        {
            if (obj is WishlistInfo that)
            {
                return this.Equals(that);
            }

            return false;
        }

        #endregion

        public override int GetHashCode() => Id.GetHashCode();
    }
}
