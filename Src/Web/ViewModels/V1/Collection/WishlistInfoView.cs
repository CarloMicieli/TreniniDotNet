using System;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
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
