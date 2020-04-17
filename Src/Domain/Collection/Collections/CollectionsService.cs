using NodaMoney;
using NodaTime;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionsService
    {
        private readonly ICollectionsFactory _factory;
        private readonly ICollectionsRepository _collections;
        private readonly ICollectionItemsRepository _collectionItems;
        private readonly IShopsRepository _shops;
        private readonly ICatalogRefsRepository _catalog;

        public CollectionsService(
            ICollectionsFactory factory,
            ICollectionsRepository collections,
            ICollectionItemsRepository collectionItems,
            IShopsRepository shops,
            ICatalogRefsRepository catalog)
        {
            _factory = factory ??
                throw new ArgumentNullException(nameof(factory));
            _collections = collections ??
                throw new ArgumentNullException(nameof(collections));
            _collectionItems = collectionItems ??
                throw new ArgumentNullException(nameof(collectionItems));
            _shops = shops ??
                throw new ArgumentNullException(nameof(shops));
            _catalog = catalog ??
                throw new ArgumentNullException(nameof(catalog));
        }

        public Task<ICollection> GetByOwnerAsync(Owner owner)
        {
            return _collections.GetByOwnerAsync(owner);
        }

        public Task<CollectionId> CreateAsync(string owner, string? notes)
        {
            var collection = _factory.NewCollection(owner);
            return Task.FromResult(collection.CollectionId);
        }

        public Task<bool> UserAlredyOwnCollectionAsync(Owner owner) =>
            _collections.AnyByOwnerAsync(owner);

        public Task<CollectionItemId> AddItemAsync(
            CollectionId id,
            ICatalogRef catalogItem,
            Condition condition,
            Money price,
            LocalDate added,
            IShopInfo? shop,
            string? notes)
        {
            var item = _factory.NewCollectionItem(catalogItem, null, condition, price, added, shop, notes);
            return _collectionItems.AddItemAsync(id, item);
        }

        public Task<CollectionId?> GetIdByOwnerAsync(Owner owner)
        {
            return _collections.GetIdByOwnerAsync(owner);
        }

        public Task<ICatalogRef> GetCatalogRefAsync(Slug catalogItemSlug)
        {
            return _catalog.GetBySlugAsync(catalogItemSlug);
        }

        public Task<bool> ExistAsync(CollectionId id)
        {
            return _collections.ExistsAsync(id);
        }

        public Task EditItemAsync(
            CollectionId id,
            CollectionItemId itemId,
            ICatalogRef catalogItem,
            Condition condition,
            Money price,
            LocalDate added,
            IShop? shop,
            string? notes)
        {
            var item = _factory.NewCollectionItem(catalogItem, null, condition, price, added, shop, notes);
            return _collectionItems.EditItemAsync(id, item);
        }

        public Task RemoveItemAsync(CollectionId collectionId, CollectionItemId itemId, LocalDate? removed, string? notes)
        {
            return _collectionItems.RemoveItemAsync(collectionId, itemId, removed, notes);
        }

        public Task<ICollectionItem> GetItemByIdAsync(CollectionId collectionId, CollectionItemId itemId)
        {
            return _collectionItems.GetItemByIdAsync(collectionId, itemId);
        }

        public Task<bool> ItemExistsAsync(CollectionId id, CollectionItemId itemId)
        {
            return _collectionItems.ItemExistsAsync(id, itemId);
        }

        public Task<IShopInfo> GetShopInfo(string shop)
        {
            return _shops.GetShopInfoBySlugAsync(Slug.Of(shop));
        }
    }
}
