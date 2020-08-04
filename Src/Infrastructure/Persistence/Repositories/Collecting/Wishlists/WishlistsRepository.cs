using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public sealed class WishlistsRepository : IWishlistsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishlistsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<WishlistId> AddAsync(Wishlist wishlist)
        {
            var param = WishlistToParam(wishlist);
            var _ = await _unitOfWork.ExecuteAsync(InsertNewWishlist, param);

            await UpdateWishlistItemsAsync(wishlist);

            return wishlist.Id;
        }

        private WishlistParam WishlistToParam(Wishlist wishlist) =>
            new WishlistParam
            {
                wishlist_id = wishlist.Id.ToGuid(),
                owner = wishlist.Owner.Value,
                slug = wishlist.Slug.Value,
                wishlist_name = wishlist.ListName,
                visibility = wishlist.Visibility.ToString(),
                budget = wishlist.Budget?.Amount,
                currency = wishlist.Budget?.Currency,
                created = wishlist.CreatedDate.ToDateTimeUtc(),
                last_modified = wishlist.ModifiedDate?.ToDateTimeUtc(),
                version = wishlist.Version
            };

        private async Task UpdateWishlistItemsAsync(Wishlist wishlist)
        {
            var resultItemIds = await GetWishlistItemIdsAsync(wishlist.Id);
            var itemIds = resultItemIds.Select(id => new WishlistItemId(id))
                .ToList();

            foreach (var item in wishlist.Items)
            {
                var param = WishlistItemToParam(wishlist.Id, item);

                if (itemIds.Contains(item.Id))
                {
                    await UpdateWishlistItemAsync(param);
                    itemIds.Remove(item.Id);
                }
                else
                {
                    await InsertWishlistItemAsync(param);
                }
            }

            foreach (var id in itemIds)
            {
                await RemoveWishlistItem(wishlist.Id, id);
            }
        }

        private Task UpdateWishlistItemAsync(WishlistItemParam param) =>
            _unitOfWork.ExecuteAsync(UpdateWishlistItem, param);

        private Task InsertWishlistItemAsync(WishlistItemParam param) =>
            _unitOfWork.ExecuteAsync(InsertWishlistItem, param);

        private Task RemoveWishlistItem(WishlistId id, WishlistItemId itemId) =>
            _unitOfWork.ExecuteAsync(DeleteWishlistItem, new
            {
                wishlist_id = id.ToGuid(),
                wishlist_item_id = itemId.ToGuid()
            });

        private WishlistItemParam WishlistItemToParam(WishlistId id, WishlistItem item) => new WishlistItemParam
        {
            wishlist_id = id.ToGuid(),
            wishlist_item_id = item.Id.ToGuid(),
            catalog_item_id = item.CatalogItem.Id.ToGuid(),
            price = item.Price?.Amount,
            currency = item.Price?.Currency,
            notes = item.Notes,
            added_date = item.AddedDate.ToDateTimeUnspecified(),
            removed_date = item.RemovedDate?.ToDateTimeUnspecified(),
            priority = item.Priority.ToString()
        };

        public async Task UpdateAsync(Wishlist wishlist)
        {
            var param = WishlistToParam(wishlist);
            var _ = await _unitOfWork.ExecuteAsync(UpdateWishlistCommand, param);

            await UpdateWishlistItemsAsync(wishlist);
        }

        public async Task DeleteAsync(WishlistId id)
        {
            await _unitOfWork.ExecuteAsync(DeleteWishlistItems, new
            {
                wishlist_id = id.ToGuid()
            });

            await _unitOfWork.ExecuteAsync(DeleteWishlist, new
            {
                wishlist_id = id.ToGuid()
            });
        }

        public async Task<List<Wishlist>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility)
        {
            if (visibility == VisibilityCriteria.All)
            {
                var results = await _unitOfWork.QueryAsync<WishlistDto>(
                    GetWishlists, new
                    {
                        Owner = owner.Value
                    });

                return ProjectToDomain(results).ToList();
            }
            else
            {
                var results = await _unitOfWork.QueryAsync<WishlistDto>(
                    GetWishlistsByVisibility, new
                    {
                        Owner = owner.Value,
                        Visibility = visibility.ToString()
                    });

                return ProjectToDomain(results).ToList();
            }
        }

        public async Task<bool> ExistsAsync(Owner owner, string listName)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<Guid?>(
                "SELECT wishlist_id FROM wishlists WHERE owner = @owner AND wishlist_name = @wishlist_name",
                new { owner = owner.Value, wishlist_name = listName });

            return result.HasValue;
        }

        public Task<int> CountWishlistsAsync(Owner owner) =>
            _unitOfWork.ExecuteScalarAsync<int>(CountWishlistsByOwner, new { owner = owner.Value });

        public async Task<bool> ExistsAsync(WishlistId id)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<Guid?>(
                "SELECT wishlist_id FROM wishlists WHERE wishlist_id = @wishlist_id",
                new { wishlist_id = id.ToGuid() });

            return result.HasValue;
        }

        public async Task<Wishlist?> GetByIdAsync(WishlistId id)
        {
            var results = await _unitOfWork.QueryAsync<WishlistDto>(
                GetWishlistById, new { wishlist_id = id.ToGuid() });

            return ProjectToDomain(results)
                .FirstOrDefault();
        }

        private IEnumerable<Wishlist> ProjectToDomain(IEnumerable<WishlistDto> results)
        {
            return results
                .GroupBy(it => new
                {
                    WishlistId = new WishlistId(it.wishlist_id),
                    Owner = new Owner(it.owner),
                    Slug = Slug.Of(it.slug),
                    WishlistName = it.wishlist_name,
                    Visibility = EnumHelpers.RequiredValueFor<Visibility>(it.visibility),
                    Budget = ToBudget(it.budget_amount, it.budget_currency),
                    it.created,
                    it.last_modified,
                    it.version
                })
                .Select(it =>
                {
                    var items = it
                        .Where(x => x.wishlist_item_id.HasValue)
                        .GroupBy(x => new
                        {
                            x.wishlist_item_id,
                            x.catalog_item_id,
                            x.catalog_item_slug,
                            x.brand_name,
                            x.item_number,
                            x.description,
                            x.priority,
                            x.added_date,
                            x.removed_date,
                            Price = ToPrice(x.price_amount, x.price_currency),
                            x.notes
                        })
                        .Select(x =>
                        {
                            var catalogItem = new CatalogItemRef(
                                new CatalogItemId(x.Key.catalog_item_id ?? Guid.Empty),
                                x.Key.catalog_item_slug!,
                                x.Key.brand_name!,
                                x.Key.item_number!,
                                x.Key.description!,
                                x.Select(y => EnumHelpers.RequiredValueFor<Category>(y.category!)));

                            return new WishlistItem(
                                new WishlistItemId(x.Key.wishlist_item_id ?? Guid.Empty),
                                catalogItem,
                                EnumHelpers.RequiredValueFor<Priority>(x.Key.priority!),
                                x.Key.added_date!.Value.ToLocalDate(),
                                x.Key.removed_date.ToLocalDateOrDefault(),
                                x.Key.Price,
                                x.Key.notes);
                        });

                    var wishlist = new Wishlist(
                        it.Key.WishlistId,
                        it.Key.Owner,
                        it.Key.WishlistName,
                        it.Key.Visibility,
                        it.Key.Budget,
                        items,
                        it.Key.created.ToUtc(),
                        it.Key.last_modified.ToUtcOrDefault(),
                        it.Key.version);
                    return wishlist;
                });
        }

        private static Price? ToPrice(decimal? amount, string? currency) =>
            (amount.HasValue && !string.IsNullOrWhiteSpace(currency)) ? new Price(amount.Value, currency) : null;

        private static Budget? ToBudget(decimal? amount, string? currency) =>
            (amount.HasValue && !string.IsNullOrWhiteSpace(currency)) ? new Budget(amount.Value, currency) : null;

        private Task<IEnumerable<Guid>> GetWishlistItemIdsAsync(WishlistId id) =>
            _unitOfWork.QueryAsync<Guid>("SELECT wishlist_item_id FROM wishlist_items WHERE wishlist_id = @Id", new
            {
                Id = id.ToGuid()
            });

        #region [ Query / Commands ]

        private const string InsertNewWishlist = @"INSERT INTO wishlists(
                wishlist_id, owner, slug, wishlist_name, visibility,
                      budget, currency, created, last_modified, version) 
            VALUES(
                @wishlist_id, @owner, @slug, @wishlist_name, @visibility, 
                   @budget, @currency, @created, @last_modified, @version);";

        private const string UpdateWishlistCommand = @"UPDATE wishlists SET 
                owner = @owner, slug = @slug, wishlist_name = @wishlist_name, visibility = @visibility, 
                budget = @budget, currency = @currency, 
                created = @created, last_modified = @last_modified, version = @version
            WHERE wishlist_id = @wishlist_id;";

        private const string InsertWishlistItem = @"INSERT INTO wishlist_items(
                 wishlist_item_id, wishlist_id, catalog_item_id, priority, added_date, price, currency, notes
             ) VALUES(
                 @wishlist_item_id, @wishlist_id, @catalog_item_id, @priority, @added_date, @price, @currency, @notes
             );";

        private const string DeleteWishlistItem = @"DELETE FROM wishlist_items 
             WHERE wishlist_item_id = @wishlist_item_id AND wishlist_id = @wishlist_id;";

        private const string UpdateWishlistItem = @"UPDATE wishlist_items
             SET 
                 priority = @priority, 
                 price = @price, 
                 currency = @currency, 
                 notes = @notes
             WHERE wishlist_item_id = @wishlist_item_id AND wishlist_id = @wishlist_id;";

        private const string DeleteWishlistItems = @"DELETE FROM wishlist_items WHERE wishlist_id = @wishlist_id";

        private const string DeleteWishlist = @"DELETE FROM wishlists WHERE wishlist_id = @wishlist_id";

        private const string GetWishlistsWithItemsSelect = @"
            SELECT w.wishlist_id, w.owner, w.slug, w.wishlist_name, w.visibility,
                   w.budget AS budget_amount, w.currency AS budget_currency, wi.wishlist_item_id, wi.catalog_item_id,
                   ci.slug AS catalog_item_slug, b.name as brand_name,
                   ci.item_number, ci.description, rs.category, wi.priority,
                   wi.added_date, wi.removed_date, wi.price AS price_amount, wi.currency AS price_currency, 
                   wi.notes, w.created, w.last_modified, w.version
            FROM wishlists AS w
            LEFT JOIN wishlist_items AS wi ON w.wishlist_id = wi.wishlist_id
            LEFT JOIN catalog_items AS ci on wi.catalog_item_id = ci.catalog_item_id
            LEFT JOIN brands AS b on ci.brand_id = b.brand_id
            LEFT JOIN rolling_stocks AS rs on ci.catalog_item_id = rs.catalog_item_id";

        private const string GetWishlistsByVisibility = GetWishlistsWithItemsSelect +
            " WHERE owner = @owner AND visibility = @visibility ORDER BY w.slug;";

        private const string GetWishlists = GetWishlistsWithItemsSelect +
            " WHERE owner = @owner ORDER BY w.slug;";

        private const string GetWishlistById = GetWishlistsWithItemsSelect +
                " WHERE w.wishlist_id = @wishlist_id;";

        private const string WishlistExistsForOwnerAndSlug = @"SELECT wishlist_id
            FROM wishlists 
            WHERE owner = @Owner AND slug = @Slug 
            LIMIT 1;";

        private const string WishlistExistsForId = @"SELECT wishlist_id
            FROM wishlists 
            WHERE owner = @owner AND wishlist_id = @wishlist_id
            LIMIT 1;";

        private const string CountWishlistsByOwner = @"SELECT COUNT(*) 
            FROM wishlists
            WHERE owner = @owner;";

        #endregion
    }
}
