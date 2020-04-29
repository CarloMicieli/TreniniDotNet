using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NodaMoney;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting.Collections
{
    public sealed class CollectionsRepository : ICollectionsRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly ICollectionsFactory _collectionsFactory;

        public CollectionsRepository(IDatabaseContext dbContext, ICollectionsFactory collectionsFactory)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _collectionsFactory = collectionsFactory ??
                throw new ArgumentNullException(nameof(collectionsFactory));
        }

        public async Task<CollectionId> AddAsync(ICollection collection)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteAsync(InsertNewCollection, new
            {
                CollectionId = collection.CollectionId.ToGuid(),
                Owner = collection.Owner.Value,
                Created = collection.CreatedDate.ToDateTimeUtc(),
                LastModified = collection.ModifiedDate?.ToDateTimeUtc(),
                collection.Version
            });

            return collection.CollectionId;
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
                new { Owner = owner.Value, CollectionId = id.ToGuid() });

            return result.HasValue;
        }

        public async Task<ICollection?> GetByOwnerAsync(Owner owner)
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

        private ICollection ProjectToDomain(CollectionDto dto, IEnumerable<CollectionItemWithDataDto> itemsDto)
        {
            IImmutableList<ICollectionItem> items = itemsDto
                .Select(it => ProjectToDomain(it))
                .ToImmutableList();

            return _collectionsFactory.NewCollection(dto.collection_id, dto.owner,
                items,
                dto.created,
                dto.last_modified,
                dto.version);
        }

        private ICollectionItem ProjectToDomain(CollectionItemWithDataDto dto)
        {
            IShopInfo? shopInfo = null;
            if (ShopInfo.TryCreate(dto.shop_id, dto.shop_name, dto.shop_slug, out var info))
            {
                shopInfo = info;
            }

            var catalogRef = CatalogRef.Of(dto.catalog_item_id, dto.catalog_item_slug);

            var details = ProjectToCatalogItemDetails(dto);

            return _collectionsFactory.NewCollectionItem(
                new CollectionItemId(dto.item_id),
                catalogRef,
                details,
                EnumHelpers.RequiredValueFor<Condition>(dto.condition),
                new Money(dto.price, dto.currency),
                dto.added_date.ToLocalDate(),
                shopInfo,
                dto.notes);
        }

        private static ICatalogItemDetails ProjectToCatalogItemDetails(CollectionItemWithDataDto dto)
        {
            IBrandRef brand = new BrandRef(dto.brand_name, Slug.Of(dto.brand_slug));
            ItemNumber itemNumber = new ItemNumber(dto.item_number);

            CollectionCategory category =
                CollectionCategories.From(dto.category_1, dto.category_2);

            IScaleRef scale = new ScaleRef(dto.scale_name, Slug.Of(dto.scale_slug));

            return new CatalogItemDetails(
                brand, itemNumber,
                category,
                scale,
                dto.rolling_stock_count,
                dto.description);
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
