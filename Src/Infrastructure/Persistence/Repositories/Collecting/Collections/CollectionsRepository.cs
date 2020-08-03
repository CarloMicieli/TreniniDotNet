using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
    public sealed class CollectionsRepository : ICollectionsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CollectionsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task DeleteAsync(CollectionId id)
        {
            await _unitOfWork.ExecuteAsync("DELETE FROM collection_items WHERE collection_id = @collection_id",
                new { collection_id = id.ToGuid() });

            await _unitOfWork.ExecuteAsync("DELETE FROM collections WHERE collection_id = @collection_id",
                new { collection_id = id.ToGuid() });
        }

        public async Task<bool> ExistsAsync(Owner owner)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<Guid?>(
                CollectionExistsByOwnerQuery,
                new { owner = owner.Value });

            return result.HasValue;
        }

        public async Task<Collection?> GetByIdAsync(CollectionId id)
        {
            var results = await _unitOfWork.QueryAsync<CollectionDto>(
                GetCollectionByIdQuery,
                new { id = id.ToGuid() });

            return ProjectToDomain(results);
        }

        public async Task<Collection?> GetByOwnerAsync(Owner owner)
        {
            var results = await _unitOfWork.QueryAsync<CollectionDto>(
                GetCollectionByOwnerQuery,
                new { owner = owner.Value });

            return ProjectToDomain(results);
        }

        public async Task UpdateAsync(Collection collection)
        {
            var param = CollectionToParam(collection);

            var _ = await _unitOfWork.ExecuteAsync(UpdateCollection, param);

            await UpdateCollectionItemsAsync(collection);
        }

        public async Task<CollectionId> AddAsync(Collection collection)
        {
            var param = CollectionToParam(collection);

            var _ = await _unitOfWork.ExecuteAsync(InsertNewCollection, param);

            await UpdateCollectionItemsAsync(collection);

            return collection.Id;
        }

        private static CollectionParam CollectionToParam(Collection collection) =>
            new CollectionParam
            {
                collection_id = collection.Id.ToGuid(),
                owner = collection.Owner.Value,
                notes = collection.Notes,
                created = collection.CreatedDate.ToDateTimeUtc(),
                last_modified = collection.ModifiedDate?.ToDateTimeUtc(),
                version = collection.Version
            };

        private async Task UpdateCollectionItemsAsync(Collection collection)
        {
            var itemIdsResults = await GetCollectionItemIdsAsync(collection.Id);
            var itemIds = itemIdsResults.Select(id => new CollectionItemId(id))
                .ToList();

            foreach (var item in collection.Items)
            {
                var param = new CollectionItemDto
                {
                    catalog_item_id = item.CatalogItem.Id.ToGuid(),
                    collection_id = collection.Id.ToGuid(),
                    collection_item_id = item.Id.ToGuid(),
                    added_date = item.AddedDate.ToDateTimeUnspecified(),
                    removed_date = item.RemovedDate?.ToDateTimeUnspecified(),
                    condition = item.Condition.ToString(),
                    price = item.Price.Amount,
                    currency = item.Price.Currency,
                    notes = item.Notes,
                    purchased_at = item.PurchasedAt?.Id.ToGuid()
                };

                if (itemIds.Contains(item.Id))
                {
                    await UpdateCollectionItemAsync(param);
                    itemIds.Remove(item.Id);
                }
                else
                {
                    await InsertCollectionItemAsync(param);
                }
            }

            // Remove all items not present anymore
            foreach (var itemId in itemIds)
            {
                await DeleteCollectionItemAsync(collection.Id, itemId);
            }
        }

        private Task UpdateCollectionItemAsync(CollectionItemDto param) =>
            _unitOfWork.ExecuteAsync(UpdateCollectionItem, param);

        private Task InsertCollectionItemAsync(CollectionItemDto param) =>
            _unitOfWork.ExecuteAsync(InsertCollectionItem, param);

        private Task DeleteCollectionItemAsync(CollectionId id, CollectionItemId itemId) =>
            _unitOfWork.ExecuteAsync(DeleteCollectionItem, new
            {
                collection_id = id.ToGuid(),
                collection_item_id = itemId.ToGuid()
            });

        private Collection? ProjectToDomain(IEnumerable<CollectionDto> results)
        {
            return results
                .GroupBy(it => new
                {
                    it.collection_id,
                    it.owner,
                    it.collection_notes,
                    it.created,
                    it.last_modified,
                    it.version
                })
                .Select(it =>
                {
                    var items = it
                        .Where(y => y.collection_item_id.HasValue && y.catalog_item_id.HasValue)
                        .GroupBy(y => new
                        {
                            ItemId = new CollectionItemId(y.collection_item_id ?? Guid.Empty),
                            CatalogItemId = new CatalogItemId(y.catalog_item_id ?? Guid.Empty),
                            CatalogItemSlug = Slug.Of(y.catalog_item_slug!),
                            ItemNumber = y.item_number,
                            Condition = y.condition,
                            BrandName = y.brand_name,
                            Description = y.description,
                            AddedDate = y.added_date!.Value.ToLocalDate(),
                            RemovedDate = y.removed_date.ToLocalDateOrDefault(),
                            Notes = y.notes,
                            Shop = ToShopRef(y.shop_id, y.shop_slug, y.shop_name),
                            Price = ToPrice(y.price, y.currency)
                        })
                        .Select(z =>
                        {
                            var k = z.Key;

                            var catalogItem = new CatalogItemRef(
                                k.CatalogItemId,
                                k.CatalogItemSlug,
                                k.BrandName!,
                                k.ItemNumber!,
                                k.Description!,
                                z.Select(y => EnumHelpers.RequiredValueFor<Category>(y.category!)));

                            return new CollectionItem(k.ItemId,
                                catalogItem,
                                EnumHelpers.RequiredValueFor<Condition>(k.Condition!),
                                k.Price,
                                k.Shop,
                                k.AddedDate,
                                k.RemovedDate,
                                k.Notes);
                        });

                    var collection = new Collection(
                        new CollectionId(it.Key.collection_id),
                        new Owner(it.Key.owner),
                        it.Key.collection_notes,
                        items,
                        it.Key.created.ToUtc(),
                        it.Key.last_modified.ToUtcOrDefault(),
                        it.Key.version);

                    return collection;
                })
                .FirstOrDefault();
        }

        private static ShopRef? ToShopRef(Guid? shopId, string? slug, string? name)
        {
            ShopRef? shop = null;
            if (shopId.HasValue)
            {
                shop = new ShopRef(new ShopId(shopId.Value), slug ?? "", name ?? "");
            }

            return shop;
        }

        private Task<IEnumerable<Guid>> GetCollectionItemIdsAsync(CollectionId id) =>
            _unitOfWork.QueryAsync<Guid>(GetCollectionItemIdsQuery, new { Id = id.ToGuid() });

        private static Price ToPrice(decimal? amount, string? currency)
        {
            return (amount.HasValue && string.IsNullOrWhiteSpace(currency) == false)
                ? new Price(amount.Value, currency)
                : throw new InvalidOperationException("Price is required - it was null instead");
        }

        #region [ Query / Commands ]

        private const string UpdateCollection = @"UPDATE collections SET 
               owner = @owner, 
               notes = @notes,
               created = @created, 
               last_modified = @last_modified, 
               version = @version
            WHERE collection_id = @collection_id;";

        private const string InsertNewCollection = @"INSERT INTO collections(
                collection_id, owner, notes, created, last_modified, version)
            VALUES(@collection_id, @owner, @notes, @created, @last_modified, @version);";

        private const string CollectionExistsByOwnerQuery = @"SELECT collection_id FROM collections WHERE owner = @owner LIMIT 1;";

        private const string GetCollectionItemIdsQuery =
            @"SELECT collection_item_id FROM collection_items WHERE collection_id = @Id";

        private const string GetCollectionIdByOwnerQuery = @"SELECT collection_id FROM collections WHERE owner = @Owner LIMIT 1;";

        private const string GetCollectionQuery = @"
            SELECT c.collection_id, c.owner, c.notes AS collection_notes,
                   items.collection_item_id, items.condition, items.price, items.currency,
                   items.added_date, items.removed_date, items.notes,
                   s.shop_id, s.name AS shop_name, s.slug AS shop_slug,
                   ci.catalog_item_id, ci.slug AS catalog_item_slug, b.name AS brand_name, ci.item_number, ci.description, rs.category,
                   c.created, c.last_modified, c.version
            FROM collections AS c
            LEFT JOIN collection_items AS items on c.collection_id = items.collection_id
            LEFT JOIN catalog_items ci on items.catalog_item_id = ci.catalog_item_id
            LEFT JOIN rolling_stocks rs on ci.catalog_item_id = rs.catalog_item_id
            LEFT JOIN brands AS b on ci.brand_id = b.brand_id
            LEFT JOIN shops AS s on items.purchased_at = s.shop_id";

        private const string GetCollectionByOwnerQuery = GetCollectionQuery + " WHERE owner = @Owner;";

        private const string GetCollectionByIdQuery = GetCollectionQuery + " WHERE collection_id = @CollectionId;";

        private const string UpdateCollectionItem = @"UPDATE collection_items 
             SET 
                 condition = @condition, 
                 price = @price, 
                 currency = @currency, 
                 purchased_at = @purchased_at, 
                 added_date = @added_date,
                 removed_date = @removed_date, 
                 notes = @notes
             WHERE collection_item_id = @collection_item_id 
               AND collection_id = @collection_id;";

        private const string DeleteCollectionItem = @"DELETE FROM collection_items 
             WHERE collection_item_id = @collection_item_id 
               AND collection_id = @collection_id;";

        private const string InsertCollectionItem = @"INSERT INTO collection_items(
                 collection_item_id, collection_id, catalog_item_id, condition, 
                             price, currency, purchased_at, added_date, removed_date, notes)
             VALUES(
                 @collection_item_id, @collection_id, @catalog_item_id, @condition, 
                    @price, @currency, @purchased_at, @added_date, NULL, @notes);";

        #endregion
    }
}
