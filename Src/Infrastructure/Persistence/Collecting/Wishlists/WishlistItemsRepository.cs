using System;
using System.Threading.Tasks;
using Dapper;
using NodaMoney;
using NodaTime;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Wishlists
{
    public sealed class WishlistItemsRepository : IWishlistItemsRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IWishlistsFactory _wishlistsFactory;
        private readonly IClock _clock;

        public WishlistItemsRepository(IDatabaseContext dbContext, IWishlistsFactory wishlistsFactory, IClock clock)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _wishlistsFactory = wishlistsFactory ??
                throw new ArgumentNullException(nameof(wishlistsFactory));
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
        }

        public async Task<WishlistItemId> AddItemAsync(WishlistId id, IWishlistItem newItem)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertWishlistItem, new
            {
                ItemId = newItem.ItemId.ToGuid(),
                WishlistId = id.ToGuid(),
                CatalogItemId = newItem.CatalogItem.CatalogItemId.ToGuid(),
                CatalogItemSlug = newItem.CatalogItem.Slug.Value,
                Priority = newItem.Priority.ToString(),
                AddedDate = newItem.AddedDate.ToDateTimeUnspecified(),
                Price = newItem.Price?.Amount,
                Currency = newItem.Price?.Currency.Code,
                newItem.Notes
            });

            return newItem.ItemId;
        }

        public async Task DeleteItemAsync(WishlistId id, WishlistItemId itemId)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var _ = await connection.ExecuteAsync(DeleteWishlistItem, new
            {
                ItemId = itemId.ToGuid(),
                WishlistId = id.ToGuid()
            });
        }

        public async Task EditItemAsync(WishlistId id, IWishlistItem modifiedItem)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var _ = await connection.ExecuteAsync(UpdateWishlistItem, new
            {
                ItemId = modifiedItem.ItemId.ToGuid(),
                WishlistId = id.ToGuid(),
                Priority = modifiedItem.Priority.ToString(),
                Price = modifiedItem.Price?.Amount,
                Currency = modifiedItem.Price?.Currency.Code,
                modifiedItem.Notes
            });
        }

        public async Task<WishlistItemId?> GetItemIdByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<Guid?>(
                GetItemIdByCatalogItem, new { WishlistId = id.ToGuid(), CatalogItemSlug = catalogRef.Slug.Value });

            return result.HasValue ? new WishlistItemId(result.Value) : (WishlistItemId?)null;
        }

        public async Task<IWishlistItem?> GetItemByIdAsync(WishlistId id, WishlistItemId itemId)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<WishlistItemDto?>(
                GetWishlistItemById, new { WishlistId = id.ToGuid(), ItemId = itemId.ToGuid() });

            if (result is null)
            {
                return null;
            }

            return ProjectToDomain(result);
        }

        private IWishlistItem ProjectToDomain(WishlistItemDto dto)
        {
            Money? price = null;
            if (dto.price.HasValue && !string.IsNullOrWhiteSpace(dto.currency))
            {
                price = new Money(dto.price.Value, dto.currency);
            }

            return _wishlistsFactory.NewWishlistItem(
                new WishlistItemId(dto.wishlist_id),
                CatalogRef.Of(dto.catalog_item_id, dto.catalog_item_slug),
                null,
                EnumHelpers.RequiredValueFor<Priority>(dto.priority),
                dto.added_date.ToLocalDate(),
                price,
                dto.notes);
        }


        #region [ Query / Commands ]

        private const string InsertWishlistItem = @"INSERT INTO wishlist_items(
                item_id, wishlist_id, catalog_item_id, catalog_item_slug, priority, added_date, price, currency, notes
            ) VALUES(
                @ItemId, @WishlistId, @CatalogItemId, @CatalogItemSlug, @Priority, @AddedDate, @Price, @Currency, @Notes
            );";

        private const string DeleteWishlistItem = @"DELETE FROM wishlist_items 
            WHERE item_id = @ItemId AND wishlist_id = @WishlistId;";

        private const string UpdateWishlistItem = @"UPDATE wishlist_items
            SET 
                priority = @Priority, 
                price = @Price, 
                currency = @Currency, 
                notes = @Notes
            WHERE item_id = @ItemId AND wishlist_id = @WishlistId;";

        private const string GetItemIdByCatalogItem = @"SELECT item_id
            FROM wishlist_items
            WHERE wishlist_id = @WishlistId
            AND catalog_item_slug = @CatalogItemSlug 
            LIMIT 1;";

        private const string GetWishlistItemById = @"SELECT *
            FROM wishlist_items 
            WHERE item_id = @ItemId AND wishlist_id = @WishlistId
            LIMIT 1;";

        #endregion
    }
}
