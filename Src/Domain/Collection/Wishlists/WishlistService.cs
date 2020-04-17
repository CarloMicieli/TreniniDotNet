using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistService
    {
        private readonly IWishlistsRepository _wishlists;
        public readonly IWishlistsFactory _wishlistsFactory;

        public WishlistService(IWishlistsRepository wishlists, IWishlistsFactory wishlistsFactory)
        {
            _wishlists = wishlists ??
                throw new ArgumentNullException(nameof(wishlists));
            _wishlistsFactory = wishlistsFactory ??
                throw new ArgumentNullException(nameof(wishlistsFactory));
        }

        public Task<bool> ExistAsync(Owner owner, Slug wishlistSlug) =>
            _wishlists.ExistAsync(owner, wishlistSlug);

        public Task<WishlistId> CreateWishlist(Owner owner, Slug slug, string listName, Visibility visibility)
        {
            var wishList = _wishlistsFactory.NewWishlist(owner, slug, listName, visibility);
            return _wishlists.AddAsync(wishList);
        }
    }
}
