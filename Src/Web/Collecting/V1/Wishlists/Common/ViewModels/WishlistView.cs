using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
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

        public Guid Id => _wishlist.WishlistId.ToGuid();

        public string Slug => _wishlist.Slug.Value;

        public string? ListName => _wishlist.ListName;

        public string Visibility => _wishlist.Visibility.ToString();

        public string Owner => _wishlist.Owner.Value;

        public List<WishlistItemView> Items { get; }
    }
}