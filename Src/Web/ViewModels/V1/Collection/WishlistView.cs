using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class WishlistView
    {
        private readonly IWishlist _wishlist;

        public WishlistView(IWishlist wishlist)
        {
            _wishlist = wishlist;

            Items = wishlist.Items
                .Select(it => new WishlistItemView(it))
                .ToList();
        }

        public Guid WishlistId => _wishlist.WishlistId.ToGuid();

        public string Slug => _wishlist.Slug.Value;

        public string? ListName => _wishlist.ListName;

        public string Visibility => _wishlist.Visibility.ToString();

        public string Owner => _wishlist.Owner.Value;

        public List<WishlistItemView> Items { get; }
    }
}