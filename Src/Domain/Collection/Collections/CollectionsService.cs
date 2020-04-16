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
        private readonly IShopsRepository _shops;

        public CollectionsService(
            ICollectionsFactory factory,
            ICollectionsRepository collections,
            IShopsRepository shops)
        {
            _factory = factory ??
                throw new ArgumentNullException(nameof(factory));
            _collections = collections ??
                throw new ArgumentNullException(nameof(collections));
            _shops = shops ??
                throw new ArgumentNullException(nameof(shops));
        }

        public Task<CollectionId> CreateAsync(string owner, string? notes)
        {
            var collection = _factory.NewCollection(owner);
            return Task.FromResult(collection.CollectionId);
        }

        public Task<bool> UserAlredyOwnCollectionAsync(string owner) =>
            _collections.AnyByOwnerAsync(owner);

        public Task<CollectionItemId> AddItemAsync(
            CollectionId id,
            ICatalogItem catalogItem,
            Condition condition,
            Money price,
            LocalDate added,
            IShopInfo? shop,
            string? notes)
        {
            var item = _factory.NewCollectionItem(catalogItem, condition, price, added, shop, notes);
            return _collections.AddItemAsync(id, item);
        }

        public Task<ICollectionItem> GetItemByIdAsync(CollectionId collectionId, CollectionItemId itemId)
        {
            return _collections.GetCollectionItemByIdAsync(collectionId, itemId);
        }

        public Task<bool> ExistAsync(CollectionId id)
        {
            return _collections.ExistsAsync(id);
        }

        public Task EditItemAsync(
            CollectionId id,
            CollectionItemId itemId,
            ICatalogItem catalogItem,
            Condition condition,
            Money price,
            LocalDate added,
            IShop? shop,
            string? notes)
        {
            var item = _factory.NewCollectionItem(catalogItem, condition, price, added, shop, notes);
            return _collections.EditItemAsync(id, item);
        }

        public Task<IShopInfo> GetShopInfo(string shop)
        {
            return _shops.GetShopInfoBySlugAsync(Slug.Of(shop));
        }

        public Task RemoveItemAsync(Guid id, Guid itemId, LocalDate? removed, string? notes) =>
            throw new NotImplementedException();
    }
}
