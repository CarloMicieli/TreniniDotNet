using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastracture.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly ICatalogItemsFactory _factory;

        public CatalogItemRepository(IDatabaseContext dbContext, ICatalogItemsFactory factory)
        {
            _dbContext = dbContext;
            _factory = factory;
        }

        public async Task<CatalogItemId> Add(CatalogItem catalogItem)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            //TODO: implement database transactions!!!!!

            var _rows1 = await connection.ExecuteAsync(InsertNewCatalogItem, catalogItem);
            foreach (var rs in catalogItem.RollingStocks)
            {
                var _rows2 = await connection.ExecuteAsync(InsertNewRollingStock, rs);
            }

            return catalogItem.CatalogItemId;
        }

        public async Task<bool> Exists(IBrand brand, ItemNumber itemNumber)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetCatalogItemWithBrandAndItemNumberExistsQuery,
                new { @brandId = brand.BrandId, @itemNumber = itemNumber.Value });

            return string.IsNullOrEmpty(result);
        }

        public async Task<ICatalogItem?> GetBy(IBrand brand, ItemNumber itemNumber)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemByBrandAndItemNumberQuery,
                new { @brandId = brand.BrandId, @itemNumber = itemNumber.Value });

            return FromCatalogItemDto(results);
        }

        public async Task<ICatalogItem?> GetBy(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemBySlug,
                new { @slug = slug.ToString() });

            return FromCatalogItemDto(results);
        }

        private ICatalogItem? FromCatalogItemDto(IEnumerable<CatalogItemWithRelatedData> results)
        {
            if (results.Any())
            {
                var catalogItem = results
                    .GroupBy(it => new
                    {
                        it.catalog_item_id,
                        it.brand_id,
                        it.item_number,
                        it.slug,
                        it.power_method,
                        it.delivery_date,
                        it.description,
                        it.model_description,
                        it.prototype_description,
                        it.created_at,
                        it.version
                    })
                    .Select(it => _factory.NewCatalogItem(
                        it.Key.catalog_item_id,
                        it.Key.brand_id,
                        it.Key.item_number,
                        it.Key.slug,
                        it.Key.power_method,
                        it.Key.delivery_date,
                        it.Key.description,
                        it.Key.model_description,
                        it.Key.prototype_description,
                        it.Select(rs => _factory.NewRollingStock(rs.rolling_stock_id, rs.railway_id, rs.era, rs.category, rs.length, rs.class_name, rs.road_number)).ToList(),
                        it.Key.created_at,
                        it.Key.version
                        ))
                    .FirstOrDefault();
                return catalogItem;
            }
            else
            {
                return null;
            }
        }

        #region [ Query / Command text ]

        private const string GetCatalogItemByBrandAndItemNumberQuery = @"SELECT 
                ci.*, rs.rolling_stock_id, rs.era, rs.category, rs.length, rs.class_name, rs.road_number, rs.railway_id
            FROM catalog_items AS ci
	        JOIN rolling_stocks AS rs 
            ON rs.catalog_item_id = ci.catalog_item_id;
            WHERE brand_id = @brandId AND item_number = @itemNumber;";

        private const string GetCatalogItemBySlug = @"SELECT 
                ci.*, rs.rolling_stock_id, rs.era, rs.category, rs.length, rs.class_name, rs.road_number, rs.railway_id
            FROM catalog_items AS ci
	        JOIN rolling_stocks AS rs 
            ON rs.catalog_item_id = ci.catalog_item_id;
            WHERE slug = @slug;";

        private const string GetCatalogItemWithBrandAndItemNumberExistsQuery = @"SELECT TOP 1 slug FROM catalog_items WHERE brand_id = @brandId AND item_number = @itemNumber;";

        private const string InsertNewCatalogItem = @"";
        private const string InsertNewRollingStock = @"";

        #endregion
    }
}
