using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public sealed class WishlistsRepository : IWishlistsRepository
    {
        private readonly IDatabaseContext _dbContext;

        public WishlistsRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<PaginatedResult<Wishlist>> GetAllAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public async Task<WishlistId> AddAsync(Wishlist wishlist)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertNewWishlist, new
            {
                WishlistId = wishlist.Id,
                Owner = wishlist.Owner.Value,
                Slug = wishlist.Slug.Value,
                wishlist.ListName,
                Visibility = wishlist.Visibility.ToString(),
                Created = wishlist.CreatedDate.ToDateTimeUtc(),
                wishlist.Version
            });

            return wishlist.Id;
        }

        public Task UpdateAsync(Wishlist brand)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(WishlistId id)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var _rows = await connection.ExecuteAsync(DeleteWishlistItems, new
            {
                WishlistId = id
            });

            var _rows2 = await connection.ExecuteAsync(DeleteWishlist, new
            {
                WishlistId = id
            });
        }

        public Task DeleteAsync(Wishlist aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsync(Owner owner, Slug wishlistSlug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<Guid?>(
                WishlishExistsForOwnerAndSlug,
                new
                {
                    Owner = owner.Value,
                    Slug = wishlistSlug.Value
                });

            return result.HasValue;
        }

        public async Task<bool> ExistAsync(Owner owner, WishlistId id)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<Guid?>(
                WishlishExistsForId,
                new
                {
                    Owner = owner.Value,
                    WishlistId = id
                });

            return result.HasValue;
        }

        public async Task<List<Wishlist>> GetByOwnerAsync(Owner owner, VisibilityCriteria visibility)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            if (visibility == VisibilityCriteria.All)
            {
                var result = await connection.QueryAsync<WishlistDto>(GetWishlists, new
                {
                    Owner = owner.Value
                });

                return result.Select(ProjectToDomain).ToList();
            }
            else
            {
                var result = await connection.QueryAsync<WishlistDto>(GetWishlistsByVisibility, new
                {
                    Owner = owner.Value,
                    Visibility = visibility.ToString()
                });

                return result.Select(ProjectToDomain).ToList();
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
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var wishlist = await connection.QueryFirstOrDefaultAsync<WishlistDto>(
                GetWishlistById, new { WishlistId = id });

            if (wishlist is null)
            {
                return null;
            }

            var wishlistItems = await connection.QueryAsync<WishlistItemWithDataDto>(
                GetWishlistItemsWithDetails, new { WishlistId = id });

            return ProjectToDomain(wishlist, wishlistItems);
        }

        private Wishlist ProjectToDomain(WishlistDto wishlistDto)
        {
            throw new NotImplementedException();
        }
        
        private Wishlist ProjectToDomain(WishlistDto wishlist, IEnumerable<WishlistItemWithDataDto> items)
        {
            // var wishlistItems = items.Select(it => ProjectItemToDomain(it))
            //     .ToImmutableList();
            //
            // return _wishlistsFactory.NewWishlist(
            //     new WishlistId(wishlist.wishlist_id),
            //     new Owner(wishlist.owner),
            //     Slug.Of(wishlist.slug),
            //     wishlist.wishlist_name,
            //     EnumHelpers.RequiredValueFor<Visibility>(wishlist.visibility),
            //     wishlistItems,
            //     wishlist.created.ToUtc(),
            //     wishlist.last_modified.ToUtcOrDefault(),
            //     wishlist.version);
            throw new NotImplementedException();
        }

        // private IWishlistItem ProjectItemToDomain(WishlistItemWithDataDto dto)
        // {
        //     Money? price = null;
        //     if (dto.price.HasValue && !string.IsNullOrWhiteSpace(dto.currency))
        //     {
        //         price = new Money(dto.price.Value, dto.currency);
        //     }
        //
        //     return _wishlistsFactory.NewWishlistItem(
        //         new WishlistItemId(dto.item_id),
        //         CatalogRef.Of(dto.catalog_item_id, dto.catalog_item_slug),
        //         ProjectToCatalogItemDetails(dto),
        //         EnumHelpers.RequiredValueFor<Priority>(dto.priority),
        //         dto.added_date.ToLocalDate(),
        //         price,
        //         dto.notes);
        // }

        // private static ICatalogItemDetails ProjectToCatalogItemDetails(WishlistItemWithDataDto dto)
        // {
        //     IBrandRef brand = new BrandRef(dto.brand_name, Slug.Of(dto.brand_slug));
        //     ItemNumber itemNumber = new ItemNumber(dto.item_number);
        //
        //     CollectionCategory category =
        //         CollectionCategories.From(dto.category_1, dto.category_2);
        //
        //     IScaleRef scale = new ScaleRef(dto.scale_name, Slug.Of(dto.scale_slug));
        //
        //     return new CatalogItemDetails(
        //         brand, itemNumber,
        //         category,
        //         scale,
        //         dto.rolling_stock_count,
        //         dto.description);
        // }

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

        private const string DeleteWishlistItems = @"DELETE FROM wishlist_items WHERE wishlist_id = @WishlistId";

        private const string DeleteWishlist = @"DELETE FROM wishlists WHERE wishlist_id = @WishlistId";

        #endregion
    }
}
