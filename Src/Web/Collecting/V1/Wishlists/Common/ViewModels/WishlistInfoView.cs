using System;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.Common.ViewModels
{
    public sealed class WishlistInfoView
    {
        private readonly IWishlistInfo _inner;

        public WishlistInfoView(IWishlistInfo inner)
        {
            _inner = inner;
        }

        public Guid WishlistId => _inner.WishlistId.ToGuid();

        public string Slug => _inner.Slug.Value;

        public string? ListName => _inner.ListName;

        public string Visibility => _inner.Visibility.ToString();
    }
}
