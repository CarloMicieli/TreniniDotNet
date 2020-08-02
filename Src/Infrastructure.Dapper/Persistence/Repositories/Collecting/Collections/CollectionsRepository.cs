using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
    public sealed class CollectionsRepository : ICollectionsRepository
    {
        private readonly IDatabaseContext _dbContext;

        public CollectionsRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<PaginatedResult<Collection>> GetAllAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public async Task<CollectionId> AddAsync(Collection collection)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertNewCollection, new
            {
                CollectionId = collection.Id,
                Owner = collection.Owner.Value,
                Created = collection.CreatedDate.ToDateTimeUtc(),
                LastModified = collection.ModifiedDate?.ToDateTimeUtc(),
                collection.Version
            });

            return collection.Id;
        }

        public Task UpdateAsync(Collection brand)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CollectionId id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Collection aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Owner owner)
        {
            var id = await GetIdByOwnerAsync(owner);
            return id.HasValue;
        }

        public async Task<bool> ExistsAsync(Owner owner, CollectionId id)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<Guid?>(
                CollectionExistsByIdQuery,
                new { Owner = owner.Value, CollectionId = id });

            return result.HasValue;
        }

        public Task<bool> ExistsAsync(CollectionId id)
        {
            throw new NotImplementedException();
        }

        public Task<Collection?> GetByIdAsync(CollectionId id)
        {
            throw new NotImplementedException();
        }

        public async Task<Collection?> GetByOwnerAsync(Owner owner)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<CollectionDto?>(
                GetCollectionByOwnerQuery,
                new { Owner = owner.Value });

            if (result is null)
            {
                return null;
            }

            var itemsResult = await connection.QueryAsync<CollectionItemWithDataDto>(
                GetCollectionItemsQuery,
                new { CollectionId = result.collection_id });

            return ProjectToDomain(result, itemsResult);
        }

        public async Task<CollectionId?> GetIdByOwnerAsync(Owner owner)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<Guid?>(
                GetCollectionIdByOwnerQuery,
                new { Owner = owner.Value });

            return result.HasValue ?
                new CollectionId(result.Value) :
                (CollectionId?)null;
        }

        private Collection ProjectToDomain(CollectionDto dto, IEnumerable<CollectionItemWithDataDto> itemsDto)
        {
            IImmutableList<CollectionItem> items = itemsDto
                .Select(it => ProjectToDomain(it))
                .ToImmutableList();

            // return _collectionsFactory.NewCollection(dto.collection_id, dto.owner,
            //     items,
            //     dto.created,
            //     dto.last_modified,
            //     dto.version);
            
            throw new NotImplementedException();
        }

        private CollectionItem ProjectToDomain(CollectionItemWithDataDto dto)
        {
            // IShopInfo? shopInfo = null;
            // if (ShopInfo.TryCreate(dto.shop_id, dto.shop_name, dto.shop_slug, out var info))
            // {
            //     shopInfo = info;
            // }
            //
            // var catalogRef = CatalogRef.Of(dto.catalog_item_id, dto.catalog_item_slug);
            //
            // var details = ProjectToCatalogItemDetails(dto);
            //
            // return _collectionsFactory.NewCollectionItem(
            //     new CollectionItemId(dto.item_id),
            //     catalogRef,
            //     details,
            //     EnumHelpers.RequiredValueFor<Condition>(dto.condition),
            //     new Money(dto.price, dto.currency),
            //     dto.added_date.ToLocalDate(),
            //     shopInfo,
            //     dto.notes);
            throw new NotImplementedException();
        }

        #region [ Query / Commands ]

        private const string InsertNewCollection = @"INSERT INTO collections(
                collection_id, owner, created, last_modified, version)
            VALUES(@CollectionId, @Owner, @Created, @LastModified, @Version);";

        private const string GetCollectionIdByOwnerQuery = @"SELECT collection_id FROM collections WHERE owner = @Owner LIMIT 1;";

        private const string GetCollectionByOwnerQuery = @"SELECT * FROM collections WHERE owner = @Owner LIMIT 1;";

        private const string CollectionExistsByIdQuery = @"SELECT collection_id FROM collections WHERE owner = @Owner AND collection_id = @CollectionId LIMIT 1;";

        private const string GetCollectionItemsQuery = @"SELECT 
                it.item_id, it.catalog_item_id, it.catalog_item_slug, it.condition, it.price, it.currency, it.added_date, it.notes,
                it.shop_id, shop.slug AS shop_slug, shop.name AS shop_name,
                b.name AS brand_name, b.slug AS brand_slug,
                s.name AS scale_name, s.slug AS scale_slug, s.ratio AS scale_ratio,
                ci.item_number, ci.description, 
                COUNT(rs.rolling_stock_id) AS rolling_stock_count, 
                MIN(rs.category) AS category_1, 
                MAX(rs.category) AS category_2 
            FROM collection_items AS it
            JOIN catalog_items AS ci
            ON ci.catalog_item_id = it.catalog_item_id
            JOIN rolling_stocks rs
            ON rs.catalog_item_id = ci.catalog_item_id
            JOIN brands AS b
            ON b.brand_id = ci.brand_id 
            JOIN scales AS s
            ON s.scale_id = ci.scale_id 
            LEFT JOIN shops AS shop
              ON shop.shop_id = it.shop_id
            WHERE collection_id = @CollectionId AND removed_date IS NULL
            GROUP BY 
                it.item_id, it.catalog_item_id, it.catalog_item_slug, it.condition, it.price, it.added_date, it.notes,
                it.shop_id, shop.slug, shop.name,
                b.name, b.slug,
                s.name, s.slug, s.ratio,
                ci.item_number, ci.description;";

        #endregion
    }
}
