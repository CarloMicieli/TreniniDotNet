// using System;
// using System.Threading.Tasks;
// using Dapper;
// using NodaMoney;
// using NodaTime;
// using TreniniDotNet.Common.Enums;
// using TreniniDotNet.Common.Extensions;
// using TreniniDotNet.Domain.Collecting.Collections;
// using TreniniDotNet.Infrastructure.Dapper;
//
// namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
// {
//     public sealed class CollectionItemsRepository : ICollectionItemsRepository
//     {
//         private readonly IDatabaseContext _dbContext;
//         private readonly ICollectionsFactory _collectionsFactory;
//
//         public CollectionItemsRepository(IDatabaseContext dbContext, ICollectionsFactory collectionsFactory)
//         {
//             _dbContext = dbContext ??
//                 throw new ArgumentNullException(nameof(dbContext));
//             _collectionsFactory = collectionsFactory ??
//                 throw new ArgumentNullException(nameof(collectionsFactory));
//         }
//
//         public async Task<CollectionItemId> AddItemAsync(CollectionId id, ICollectionItem newItem)
//         {
//             await using var connection = _dbContext.NewConnection();
//             await connection.OpenAsync();
//
//             var result = await connection.ExecuteAsync(InsertNewCollectionItem, new
//             {
//                 ItemId = newItem.Id.ToGuid(),
//                 CollectionId = id.ToGuid(),
//                 CatalogItemId = newItem.CatalogItem.CatalogItemId.ToGuid(),
//                 CatalogItemSlug = newItem.CatalogItem.Slug.Value,
//                 Condition = newItem.Condition.ToString(),
//                 Price = newItem.Price.Amount,
//                 Currency = newItem.Price.Currency.Code,
//                 ShopId = newItem.PurchasedAt?.Id.ToGuid(),
//                 AddedDate = newItem.AddedDate.ToDateTimeUnspecified(),
//                 newItem.Notes
//             });
//
//             return newItem.Id;
//         }
//
//         public async Task EditItemAsync(CollectionId id, ICollectionItem item)
//         {
//             await using var connection = _dbContext.NewConnection();
//             await connection.OpenAsync();
//
//             var _ = await connection.ExecuteAsync(UpdateCollectionItem, new
//             {
//                 ItemId = item.Id.ToGuid(),
//                 CollectionId = id.ToGuid(),
//                 Condition = item.Condition.ToString(),
//                 Price = item.Price.Amount,
//                 Currency = item.Price.Currency.Code,
//                 ShopId = item.PurchasedAt?.Id.ToGuid(),
//                 AddedDate = item.AddedDate.ToDateTimeUnspecified(),
//                 item.Notes
//             });
//         }
//
//         public async Task<ICollectionItem?> GetItemByIdAsync(CollectionId collectionId, CollectionItemId itemId)
//         {
//             await using var connection = _dbContext.NewConnection();
//             await connection.OpenAsync();
//
//             var result = await connection.QueryFirstOrDefaultAsync<CollectionItemDto>(GetCollectionItem, new
//             {
//                 ItemId = itemId.ToGuid(),
//                 CollectionId = collectionId.ToGuid(),
//             });
//
//             if (result is null)
//             {
//                 return null;
//             }
//
//             return ProjectToDomain(result);
//         }
//
//         public async Task<bool> ItemExistsAsync(CollectionId id, CollectionItemId itemId)
//         {
//             await using var connection = _dbContext.NewConnection();
//             await connection.OpenAsync();
//
//             var result = await connection.QueryFirstOrDefaultAsync<Guid?>(CollectionItemExistsQuery, new
//             {
//                 ItemId = itemId.ToGuid(),
//                 CollectionId = id.ToGuid(),
//             });
//
//             return result.HasValue;
//         }
//
//         public async Task RemoveItemAsync(CollectionId collectionId, CollectionItemId itemId, LocalDate? removed)
//         {
//             await using var connection = _dbContext.NewConnection();
//             await connection.OpenAsync();
//
//             var _ = await connection.ExecuteAsync(RemoveCollectionItem, new
//             {
//                 ItemId = itemId.ToGuid(),
//                 CollectionId = collectionId.ToGuid(),
//                 RemovedDate = removed?.ToDateTimeUnspecified()
//             });
//         }
//
//         private ICollectionItem ProjectToDomain(CollectionItemDto dto)
//         {
//             IShopInfo? shop = null;
//             if (ShopInfo.TryCreate(dto.shop_id, dto.shop_name, dto.shop_slug, out var s))
//             {
//                 shop = s;
//             }
//
//             return _collectionsFactory.NewCollectionItem(
//                 new CollectionItemId(dto.item_id),
//                 CatalogRef.Of(dto.catalog_item_id, dto.catalog_item_slug),
//                 null,
//                 EnumHelpers.RequiredValueFor<Condition>(dto.condition),
//                 new Money(dto.price, dto.currency),
//                 dto.added_date.ToLocalDate(),
//                 shop,
//                 dto.notes);
//         }
//
//         #region [ Query / Commands ]
//
//         private const string InsertNewCollectionItem = @"INSERT INTO collection_items(
//                 item_id, collection_id, catalog_item_id, catalog_item_slug, condition, price, currency, shop_id, added_date, removed_date, notes)
//             VALUES(
//                 @ItemId, @CollectionId, @CatalogItemId, @CatalogItemSlug, @Condition, @Price, @Currency, @ShopId, @AddedDate, NULL, @Notes);";
//
//         private const string UpdateCollectionItem = @"UPDATE collection_items 
//             SET 
//                 condition = @Condition, 
//                 price = @Price, currency = @Currency, 
//                 shop_id = @ShopId, 
//                 added_date = @AddedDate, 
//                 notes = @Notes
//             WHERE item_id = @ItemId AND collection_id = @CollectionId;";
//
//         private const string CollectionItemExistsQuery = @"SELECT item_id 
//             FROM collection_items 
//             WHERE item_id = @ItemId AND collection_id = @CollectionId 
//             LIMIT 1;";
//
//         private const string RemoveCollectionItem = @"UPDATE collection_items 
//             SET
//                 removed_date = @RemovedDate
//             WHERE item_id = @ItemId AND collection_id = @CollectionId;";
//
//         private const string GetCollectionItem = @"SELECT ci.*, s.name AS shop_name, s.slug AS shop_slug
//             FROM collection_items AS ci
//             LEFT JOIN shops AS s
//             ON s.shop_id = ci.shop_id
//             WHERE item_id = @ItemId AND collection_id = @CollectionId 
//             LIMIT 1;";
//
//         #endregion
//     }
// }
