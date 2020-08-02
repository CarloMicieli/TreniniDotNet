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
            var _ = await _unitOfWork.ExecuteAsync(InsertNewWishlist, new
            {
                WishlistId = wishlist.Id,
                Owner = wishlist.Owner.Value,
                Slug = wishlist.Slug.Value,
                wishlist.ListName,
                Visibility = wishlist.Visibility.ToString(),
                Created = wishlist.CreatedDate.ToDateTimeUtc(),
                wishlist.Version
            });

            await UpdateWishlistItemsAsync(wishlist);
            
            return wishlist.Id;
        }
        
        private async Task UpdateWishlistItemsAsync(Wishlist wishlist)
        {
            var resultItemIds = await GetWishlistItemIdsAsync(wishlist.Id);
            var itemIds = resultItemIds.Select(id => new WishlistItemId(id))
                .ToList();
            
            foreach (var item in wishlist.Items)
            {
                var param = new WishlistItemDto { };
                
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

        private Task UpdateWishlistItemAsync(WishlistItemDto param) => throw new NotImplementedException();
        
        private Task InsertWishlistItemAsync(WishlistItemDto param) => throw new NotImplementedException();
        
        private Task RemoveWishlistItem(WishlistId id, WishlistItemId itemId) => throw new NotImplementedException();
        
        public async Task UpdateAsync(Wishlist wishlist)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateWishlistCommand, new
            {
                WishlistId = wishlist.Id,
                Owner = wishlist.Owner.Value,
                Slug = wishlist.Slug.Value,
                wishlist.ListName,
                Visibility = wishlist.Visibility.ToString(),
                Created = wishlist.CreatedDate.ToDateTimeUtc(),
                wishlist.Version
            });

            await UpdateWishlistItemsAsync(wishlist);
        }

        public async Task DeleteAsync(WishlistId id)
        {
            await _unitOfWork.ExecuteAsync(DeleteWishlistItems, new
            {
                WishlistId = id
            });

            await _unitOfWork.ExecuteAsync(DeleteWishlist, new
            {
                WishlistId = id
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

        public Task<bool> ExistsAsync(Owner owner, string listName)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWishlistsAsync(Owner owner)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(WishlistId id)
        {
            throw new NotImplementedException();
        }

        public async Task<Wishlist?> GetByIdAsync(WishlistId id)
        {
            var results = await _unitOfWork.QueryAsync<WishlistDto>(
                GetWishlistById, new {Id = id.ToGuid()});

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
                wishlist_id, owner, slug, wishlist_name, visibility, created, last_modified, version) 
            VALUES(
                @WishlistId, @Owner, @Slug, @ListName, @Visibility, @Created, NULL, @Version);";

        private const string GetWishlistById = @"SELECT *
            FROM wishlists
            WHERE wishlist_id = @WishlistId
            LIMIT 1;";

        private const string GetWishlistItemsWithDetails = @"SELECT
                it.item_id, it.catalog_item_id, it.catalog_item_slug, it.priority, it.added_date,
                it.price, it.currency, it.notes,
                b.name AS brand_name, b.slug AS brand_slug,
                ci.item_number, s.name AS scale_name, s.slug AS scale_slug, ci.description,
                COUNT(rolling_stock_id) AS rolling_stock_count,
                MIN(rs.category) AS category_1,
                MAX(rs.category) AS category_2
            FROM wishlist_items AS it
            JOIN catalog_items AS ci
            ON it.catalog_item_id = ci.catalog_item_id
            JOIN brands AS b
            ON b.brand_id = ci.brand_id
            JOIN scales AS s
            ON s.scale_id = ci.scale_id
            JOIN rolling_stocks AS rs
            ON rs.catalog_item_id = ci.catalog_item_id
            WHERE wishlist_id = @WishlistId
            GROUP BY 
                it.item_id, it.catalog_item_id, it.catalog_item_slug, it.priority, it.added_date,
                it.price, it.currency, it.notes,
                b.name, b.slug,
                ci.item_number, s.name, s.slug, ci.description";

        private const string GetWishlistsByVisibility = @"SELECT wishlist_id, slug, wishlist_name, visibility 
            FROM wishlists 
            WHERE owner = @Owner AND visibility = @Visibility 
            ORDER BY slug;";

        private const string GetWishlists = @"SELECT wishlist_id, slug, wishlist_name, visibility 
            FROM wishlists 
            WHERE owner = @Owner
            ORDER BY slug;";

        private const string WishlishExistsForOwnerAndSlug = @"SELECT wishlist_id
            FROM wishlists 
            WHERE owner = @Owner AND slug = @Slug 
            LIMIT 1;";

        private const string WishlishExistsForId = @"SELECT wishlist_id
            FROM wishlists 
            WHERE owner = @Owner AND wishlist_id = @WishlistId
            LIMIT 1;";

        private const string UpdateWishlistCommand = "";
        
        private const string DeleteWishlistItems = @"DELETE FROM wishlist_items WHERE wishlist_id = @WishlistId";

        private const string DeleteWishlist = @"DELETE FROM wishlists WHERE wishlist_id = @WishlistId";

        #endregion
    }
}
