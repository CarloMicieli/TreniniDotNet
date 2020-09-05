using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public sealed class WishlistsService : IDomainService
    {
        private readonly IWishlistsRepository _wishlistsRepository;
        private readonly ICatalogItemRefsRepository _catalogItemsRepository;
        private readonly WishlistsFactory _wishlistsFactory;

        public WishlistsService(
            WishlistsFactory wishlistsFactory,
            IWishlistsRepository wishlistsRepository,
            ICatalogItemRefsRepository catalogItemsRepository)
        {
            _wishlistsFactory = wishlistsFactory ?? throw new ArgumentNullException(nameof(wishlistsFactory));
            _wishlistsRepository = wishlistsRepository ?? throw new ArgumentNullException(nameof(wishlistsRepository));
            _catalogItemsRepository =
                catalogItemsRepository ?? throw new ArgumentException(nameof(catalogItemsRepository));
        }

        public Task<Wishlist?> GetByIdAsync(WishlistId id) =>
            _wishlistsRepository.GetByIdAsync(id);

        public Task<List<Wishlist>> GetWishlistsByOwnerAsync(Owner owner, VisibilityCriteria visibility) =>
            _wishlistsRepository.GetByOwnerAsync(owner, visibility);

        public Task<WishlistId> CreateWishlistAsync(Owner owner, string? listName, Visibility visibility, Budget? budget)
        {
            var wishlist = _wishlistsFactory.CreateWishlist(owner, listName, visibility, budget);
            return _wishlistsRepository.AddAsync(wishlist);
        }

        public async Task<string> GenerateWishlistName(Owner owner, string? listName)
        {
            if (listName is null)
            {
                var count = await _wishlistsRepository.CountWishlistsAsync(owner);
                return $"{owner.Value}'s wish list {count + 1}";
            }

            return listName;
        }

        public Task UpdateWishlistAsync(Wishlist wishlist) =>
            _wishlistsRepository.UpdateAsync(wishlist);

        public Task DeleteWishlistAsync(WishlistId id) =>
            _wishlistsRepository.DeleteAsync(id);

        public Task<bool> ExistsAsync(WishlistId id) =>
            _wishlistsRepository.ExistsAsync(id);

        public Task<bool> ExistsAsync(Owner owner, string listName) =>
            _wishlistsRepository.ExistsAsync(owner, listName);

        public Task<CatalogItemRef?> GetCatalogItemAsync(Slug slug) =>
            _catalogItemsRepository.GetCatalogItemAsync(slug);
    }
}
