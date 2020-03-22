using Dapper;
using System;
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

        public async Task<CatalogItemId> Add(ICatalogItem catalogItem)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            //TODO: implement database transactions!!!!!

            var _rows1 = await connection.ExecuteAsync(InsertNewCatalogItem, new
            {
                catalogItem.CatalogItemId,
                catalogItem.Brand.BrandId,
                catalogItem.Scale.ScaleId,
                catalogItem.ItemNumber,
                catalogItem.Slug,
                catalogItem.PowerMethod,
                DeliveryDate = (string?)null, //catalogItem.DeliveryDate,
                catalogItem.Description,
                catalogItem.ModelDescription,
                catalogItem.PrototypeDescription,
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            foreach (var rs in catalogItem.RollingStocks)
            {
                var _rows2 = await connection.ExecuteAsync(InsertNewRollingStock, new
                {
                    rs.RollingStockId,
                    rs.Era,
                    rs.Category,
                    RailwayId = rs.Railway.RailwayId,
                    catalogItem.CatalogItemId,
                    rs.Length,
                    rs.ClassName,
                    rs.RoadNumber
                });
            }

            return catalogItem.CatalogItemId;
        }

        public async Task<bool> Exists(IBrandInfo brand, ItemNumber itemNumber)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetCatalogItemWithBrandAndItemNumberExistsQuery,
                new { @brandId = brand.BrandId, @itemNumber = itemNumber.Value });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<ICatalogItem?> GetBy(IBrandInfo brand, ItemNumber itemNumber)
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
                        brand = new BrandInfo(it.brand_id, it.brand_slug, it.brand_name),
                        scale = new ScaleInfo(it.scale_id, it.scale_slug, it.scale_name, it.scale_ratio),
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
                        it.Key.brand,
                        it.Key.item_number,
                        it.Key.slug,
                        it.Key.scale,
                        it.Key.power_method,
                        it.Key.delivery_date,
                        it.Key.description,
                        it.Key.model_description,
                        it.Key.prototype_description,
                        it.Select(rs =>
                        {
                            var railway = new RailwayInfo(rs.railway_id, rs.railway_slug, rs.railway_name, rs.railway_country);
                            return _factory.NewRollingStock(
                                rs.rolling_stock_id, railway, rs.era, rs.category, rs.length, rs.class_name, rs.road_number);
                        }).ToList(),
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
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, 
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length, rs.class_name, rs.road_number,
                b.brand_id, b.name as brand_name, b.slug as brand_slug,
                r.railway_id, r.name as railway_name, r.slug as railway_slug, r.country as railway_country,
                s.scale_id, s.name as scale_name, s.slug as scale_slug, s.ratio as scale_ratio,
                ci.created_at, ci.version
            FROM catalog_items AS ci
            JOIN brands AS b
            ON b.brand_id = ci.brand_id 
            JOIN scales AS s
            ON s.scale_id = ci.scale_id 
	        JOIN rolling_stocks AS rs 
            ON rs.catalog_item_id = ci.catalog_item_id
            JOIN railways AS r
            ON r.railway_id = rs.railway_id
            WHERE b.brand_id = @brandId AND ci.item_number = @itemNumber;";

        private const string GetCatalogItemBySlug = @"SELECT 
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, 
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length, rs.class_name, rs.road_number,
                b.brand_id, b.name as brand_name, b.slug as brand_slug,
                r.railway_id, r.name as railway_name, r.slug as railway_slug, r.country as railway_country,
                s.scale_id, s.name as scale_name, s.slug as scale_slug, s.ratio as scale_ratio,
                ci.created_at, ci.version
            FROM catalog_items AS ci
            JOIN brands AS b
            ON b.brand_id = ci.brand_id 
            JOIN scales AS s
            ON s.scale_id = ci.scale_id 
	        JOIN rolling_stocks AS rs 
            ON rs.catalog_item_id = ci.catalog_item_id
            JOIN railways AS r
            ON r.railway_id = rs.railway_id
            WHERE slug = @slug;";

        private const string GetCatalogItemWithBrandAndItemNumberExistsQuery = @"SELECT slug FROM catalog_items WHERE brand_id = @brandId AND item_number = @itemNumber LIMIT 1;";

        private const string InsertNewCatalogItem = @"INSERT INTO catalog_items(
	            catalog_item_id, brand_id, scale_id, item_number, slug, power_method, delivery_date, 
                description, model_description, prototype_description, created_at, version)
            VALUES(@CatalogItemId, @BrandId, @ScaleId, @ItemNumber, @Slug, @PowerMethod, @DeliveryDate, 
                @Description, @ModelDescription, @PrototypeDescription, @CreatedAt, @Version);";

        private const string InsertNewRollingStock = @"INSERT INTO rolling_stocks(
	            rolling_stock_id, era, category, railway_id, catalog_item_id, length, class_name, road_number)
	        VALUES(@RollingStockId, @Era, @Category, @RailwayId, @CatalogItemId, @Length, @ClassName, @RoadNumber);";

        #endregion
    }
}
