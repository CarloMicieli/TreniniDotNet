using System;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public sealed class WishlistInfoView
    {
        private Wishlist Wishlist { get; }

        public WishlistInfoView(Wishlist wishlist)
        {
            Wishlist = wishlist;
        }

        public Guid Id => Wishlist.Id;

        public string Slug => Wishlist.Slug.Value;

        public string? ListName => Wishlist.ListName;

        public string Visibility => Wishlist.Visibility.ToString();
    }
}
