using NodaMoney;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistService
    {
        private readonly IWishlistsRepository _wishlists;
        private readonly IWishlistItemsRepository _wishlistItems;
        public readonly IWishlistsFactory _wishlistsFactory;
        private readonly ICatalogRefsRepository _catalog;

        public WishlistService(
            IWishlistsRepository wishlists,
            IWishlistItemsRepository wishlistItems,
            IWishlistsFactory wishlistsFactory,
            ICatalogRefsRepository catalog)
        {
            _wishlists = wishlists ??
                throw new ArgumentNullException(nameof(wishlists));
            _wishlistItems = wishlistItems ??
                throw new ArgumentNullException(nameof(wishlistItems));
            _wishlistsFactory = wishlistsFactory ??
                throw new ArgumentNullException(nameof(wishlistsFactory));
            _catalog = catalog ??
                throw new ArgumentNullException(nameof(catalog));
        }

        public Task<IEnumerable<IWishlistInfo>> GetByOwnerAsync(Owner owner, Visibility visibility) =>
            _wishlists.GetByOwnerAsync(owner, visibility);

        public Task<IWishlist> GetByIdAsync(WishlistId id) =>
            _wishlists.GetByIdAsync(id);

        public Task<bool> ExistAsync(WishlistId id) =>
            _wishlists.ExistAsync(id);

        public Task<IWishlistItem> GetItemByIdAsync(WishlistId id, WishlistItemId itemId) =>
            _wishlistItems.GetItemByIdAsync(id, itemId);

        public Task DeleteAsync(WishlistId id) =>
            _wishlists.DeleteAsync(id);

        public Task<bool> ExistAsync(Owner owner, Slug wishlistSlug) =>
            _wishlists.ExistAsync(owner, wishlistSlug);

        public Task DeleteItemAsync(WishlistId id, WishlistItemId itemId) =>
            _wishlistItems.DeleteItemAsync(id, itemId);

        public Task<WishlistId> CreateWishlist(Owner owner, Slug slug, string listName, Visibility visibility)
        {
            var wishList = _wishlistsFactory.NewWishlist(owner, slug, listName, visibility);
            return _wishlists.AddAsync(wishList);
        }

        public Task EditItemAsync(WishlistId id, IWishlistItem item, LocalDate? addedDate, Money? price, Priority? priority, string? notes)
        {
            var modifiedItem = _wishlistsFactory.NewWishlistItem(
                item.ItemId,
                item.CatalogItem,
                null,
                priority ?? item.Priority,
                addedDate ?? item.AddedDate,
                price ?? item.Price,
                notes ?? item.Notes);

            return _wishlistItems.EditItemAsync(id, modifiedItem);
        }

        public Task<ICatalogRef> GetCatalogRef(Slug catalogItemSlug) =>
            _catalog.GetBySlugAsync(catalogItemSlug);

        public Task<IWishlistItem> GetItemByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef) =>
            _wishlistItems.GetItemByCatalogRefAsync(id, catalogRef);

        public Task<WishlistItemId> AddItemAsync(
            WishlistId id,
            ICatalogRef catalogRef,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes)
        {
            var newItem = _wishlistsFactory.NewWishlistItem(catalogRef, priority, addedDate, price, notes);
            return _wishlistItems.AddItemAsync(id, newItem);
        }
    }
}
