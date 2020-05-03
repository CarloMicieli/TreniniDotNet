using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly IDatabaseContext _dbContext;
        private readonly ICatalogItemsFactory _factory;
        private readonly IRollingStocksFactory _rsFactory;

        public CatalogItemRepository(IDatabaseContext dbContext, ICatalogItemsFactory factory, IRollingStocksFactory rsFactory)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            _factory = factory ??
                throw new ArgumentNullException(nameof(factory));
            _rsFactory = rsFactory ??
                throw new ArgumentNullException(nameof(rsFactory));
        }

        public async Task<CatalogItemId> AddAsync(ICatalogItem catalogItem)
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
                DeliveryDate = catalogItem.DeliveryDate?.ToString(),
                Available = catalogItem.IsAvailable,
                catalogItem.Description,
                catalogItem.ModelDescription,
                catalogItem.PrototypeDescription,
                Created = catalogItem.CreatedDate.ToDateTimeUtc(),
                Modified = catalogItem.ModifiedDate?.ToDateTimeUtc(),
                catalogItem.Version
            });

            foreach (var rs in catalogItem.RollingStocks)
            {
                var _rows2 = await connection.ExecuteAsync(InsertNewRollingStock, new
                {
                    rs.RollingStockId,
                    Era = rs.Epoch.ToString(),
                    rs.Category,
                    rs.Railway.RailwayId,
                    catalogItem.CatalogItemId,
                    LengthMm = rs.Length?.Millimeters,
                    LengthIn = rs.Length?.Inches,
                    rs.ClassName,
                    rs.RoadNumber,
                    rs.TypeName,
                    PassengerCarType = rs.PassengerCarType?.ToString(),
                    ServiceLevel = rs.ServiceLevel?.ToString(),
                    DccInterface = rs.DccInterface.ToString(),
                    Control = rs.Control.ToString()
                });
            }

            return catalogItem.CatalogItemId;
        }

        public async Task UpdateAsync(ICatalogItem catalogItem)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var _rows1 = await connection.ExecuteAsync(UpdateCatalogItemCommand, new
            {
                catalogItem.CatalogItemId,
                catalogItem.Brand.BrandId,
                catalogItem.Scale.ScaleId,
                catalogItem.ItemNumber,
                catalogItem.Slug,
                catalogItem.PowerMethod,
                DeliveryDate = catalogItem.DeliveryDate?.ToString(),
                Available = catalogItem.IsAvailable,
                catalogItem.Description,
                catalogItem.ModelDescription,
                catalogItem.PrototypeDescription,
                Modified = catalogItem.ModifiedDate?.ToDateTimeUtc(),
                catalogItem.Version
            });

            var _rows2 = await connection.ExecuteAsync(DeleteAllRollingStocks, new
            {
                catalogItem.CatalogItemId
            });

            foreach (var rs in catalogItem.RollingStocks)
            {
                var _rows3 = await connection.ExecuteAsync(InsertNewRollingStock, new
                {
                    rs.RollingStockId,
                    Era = rs.Epoch.ToString(),
                    rs.Category,
                    rs.Railway.RailwayId,
                    catalogItem.CatalogItemId,
                    LengthMm = rs.Length?.Millimeters,
                    LengthIn = rs.Length?.Inches,
                    rs.ClassName,
                    rs.RoadNumber,
                    rs.TypeName,
                    PassengerCarType = rs.PassengerCarType?.ToString(),
                    ServiceLevel = rs.ServiceLevel?.ToString(),
                    DccInterface = rs.DccInterface.ToString(),
                    Control = rs.Control.ToString()
                });
            }
        }

        public async Task<bool> ExistsAsync(IBrandInfo brand, ItemNumber itemNumber)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var result = await connection.ExecuteScalarAsync<string>(
                GetCatalogItemWithBrandAndItemNumberExistsQuery,
                new { @brandId = brand.BrandId, @itemNumber = itemNumber.Value });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<ICatalogItem?> GetByBrandAndItemNumberAsync(IBrandInfo brand, ItemNumber itemNumber)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemByBrandAndItemNumberQuery,
                new { @brandId = brand.BrandId, @itemNumber = itemNumber.Value });

            return FromCatalogItemDto(results);
        }

        public async Task<ICatalogItem?> GetBySlugAsync(Slug slug)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemBySlug,
                new { @slug = slug.ToString() });

            return FromCatalogItemDto(results);
        }

        public async Task<PaginatedResult<ICatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            await using var connection = _dbContext.NewConnection();
            await connection.OpenAsync();

            var results = await connection.QueryAsync<CatalogItemWithRelatedData>(
                GetLatestCatalogItemsQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<ICatalogItem>(page, FromCatalogItemsDto(results));
        }

        public Task AddRollingStockAsync(CatalogItemId catalogItemId, IRollingStock rollingStock)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<ICatalogItem> FromCatalogItemsDto(IEnumerable<CatalogItemWithRelatedData> results)
        {
            return results
                .GroupBy(it => new CatalogItemGroupingKey
                {
                    catalog_item_id = it.catalog_item_id,
                    brand_id = it.brand_id,
                    scale_id = it.scale_id,
                    item_number = it.item_number,
                    slug = it.slug
                })
                .Select(it => ProjectToCatalogItem(it))
                .ToList();
        }

        private ICatalogItem? FromCatalogItemDto(IEnumerable<CatalogItemWithRelatedData> results)
        {
            if (results.Any())
            {
                var catalogItem = results
                    .GroupBy(it => new CatalogItemGroupingKey
                    {
                        catalog_item_id = it.catalog_item_id,
                        brand_id = it.brand_id,
                        scale_id = it.scale_id,
                        item_number = it.item_number,
                        slug = it.slug
                    })
                    .Select(it => ProjectToCatalogItem(it))
                    .FirstOrDefault();
                return catalogItem;
            }
            else
            {
                return null;
            }
        }

        private ICatalogItem ProjectToCatalogItem(IGrouping<CatalogItemGroupingKey, CatalogItemWithRelatedData> dto)
        {
            IReadOnlyList<IRollingStock> rollingStocks = dto
                .Select(it => this.ProjectToRollingStock(it)!)
                .ToImmutableList();

            CatalogItemWithRelatedData it = dto.First();

            return _factory.CatalogItemWith(
                it.catalog_item_id,
                it.slug,
                new BrandInfo(it.brand_id, it.brand_slug, it.brand_name),
                it.item_number,
                new ScaleInfo(it.scale_id, it.scale_slug, it.scale_name, it.scale_ratio),
                it.power_method,
                it.delivery_date,
                it.available,
                it.description,
                it.model_description,
                it.prototype_description,
                rollingStocks,
                it.created_at ?? DateTime.UtcNow,
                it.version ?? 1);
        }

        private IRollingStock? ProjectToRollingStock(CatalogItemWithRelatedData? dto)
        {
            if (dto is null)
            {
                return null;
            }
            else
            {
                var railwayInfo = ProjectToRailwayInfo(dto!);
                return RollingStock(dto!, railwayInfo);
            }
        }

        private static IRailwayInfo ProjectToRailwayInfo(CatalogItemWithRelatedData dto) =>
            new RailwayInfo(dto.railway_id, dto.railway_slug, dto.railway_name);

        private IRollingStock RollingStock(CatalogItemWithRelatedData dto, IRailwayInfo railway) =>
            _rsFactory.RollingStockWith(
                dto.rolling_stock_id,
                railway,
                dto.era,
                dto.category,
                dto.length_mm,
                dto.length_in,
                dto.class_name,
                dto.road_number,
                dto.type_name,
                dto.passenger_car_type,
                dto.service_level,
                dto.dcc_interface,
                dto.control
            );

        #region [ Query / Command text ]

        private const string GetLatestCatalogItemsQuery = @"SELECT 
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, ci.available,
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length_mm, rs.length_in, 
                rs.class_name, rs.road_number, rs.type_name, rs.passenger_car_type, rs.service_level,
                rs.dcc_interface, rs.control,
                b.brand_id, b.name as brand_name, b.slug as brand_slug,
                r.railway_id, r.name as railway_name, r.slug as railway_slug, r.country as railway_country,
                s.scale_id, s.name as scale_name, s.slug as scale_slug, s.ratio as scale_ratio,
                ci.created, ci.last_modified, ci.version
            FROM (
                SELECT * 
                FROM catalog_items
                ORDER BY created DESC 
                LIMIT @limit OFFSET @skip
            ) AS ci
            JOIN brands AS b
            ON b.brand_id = ci.brand_id 
            JOIN scales AS s
            ON s.scale_id = ci.scale_id 
	        JOIN rolling_stocks AS rs 
            ON rs.catalog_item_id = ci.catalog_item_id
            JOIN railways AS r
            ON r.railway_id = rs.railway_id;";

        private const string GetCatalogItemByBrandAndItemNumberQuery = @"SELECT 
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, ci.available,
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length_mm, rs.length_in, 
                rs.class_name, rs.road_number, rs.type_name, rs.passenger_car_type, rs.service_level,
                rs.dcc_interface, rs.control,
                b.brand_id, b.name as brand_name, b.slug as brand_slug,
                r.railway_id, r.name as railway_name, r.slug as railway_slug, r.country as railway_country,
                s.scale_id, s.name as scale_name, s.slug as scale_slug, s.ratio as scale_ratio,
                ci.created, ci.last_modified, ci.version
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
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, ci.available,
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length_mm, rs.length_in,
                rs.class_name, rs.road_number, rs.type_name, rs.passenger_car_type, rs.service_level,
                rs.dcc_interface, rs.control,
                b.brand_id, b.name as brand_name, b.slug as brand_slug,
                r.railway_id, r.name as railway_name, r.slug as railway_slug, r.country as railway_country,
                s.scale_id, s.name as scale_name, s.slug as scale_slug, s.ratio as scale_ratio,
                ci.created, ci.last_modified, ci.version
            FROM catalog_items AS ci
            JOIN brands AS b
            ON b.brand_id = ci.brand_id 
            JOIN scales AS s
            ON s.scale_id = ci.scale_id 
	        JOIN rolling_stocks AS rs 
            ON rs.catalog_item_id = ci.catalog_item_id
            JOIN railways AS r
            ON r.railway_id = rs.railway_id
            WHERE ci.slug = @slug;";

        private const string GetCatalogItemWithBrandAndItemNumberExistsQuery = @"SELECT slug FROM catalog_items WHERE brand_id = @brandId AND item_number = @itemNumber LIMIT 1;";

        private const string InsertNewCatalogItem = @"INSERT INTO catalog_items(
	            catalog_item_id, brand_id, scale_id, item_number, slug, power_method, delivery_date, available,
                description, model_description, prototype_description, created, last_modified, version)
            VALUES(@CatalogItemId, @BrandId, @ScaleId, @ItemNumber, @Slug, @PowerMethod, @DeliveryDate, @Available,
                @Description, @ModelDescription, @PrototypeDescription, @Created, @Modified, @Version);";

        private const string InsertNewRollingStock = @"INSERT INTO rolling_stocks(
	            rolling_stock_id, era, category, railway_id, catalog_item_id, length_mm, length_in,
                class_name, road_number, type_name, passenger_car_type, service_level, dcc_interface, control)
	        VALUES(@RollingStockId, @Era, @Category, @RailwayId, @CatalogItemId, @LengthMm, @LengthIn, 
                @ClassName, @RoadNumber, @TypeName, @PassengerCarType, @ServiceLevel, @DccInterface, @Control);";

        private const string UpdateCatalogItemCommand = @"UPDATE catalog_items SET 
                brand_id = @BrandId, 
                scale_id = @ScaleId, 
                item_number = @ItemNumber, 
                slug = @Slug, 
                power_method = @PowerMethod,
                delivery_date = @DeliveryDate,
                available = @Available,
                description = @Description, 
                model_description = @ModelDescription,
                prototype_description = @PrototypeDescription,
                last_modified = @Modified, 
                version = @Version
            WHERE catalog_item_id = @CatalogItemId;";

        private const string DeleteAllRollingStocks = @"DELETE FROM rolling_stocks WHERE catalog_item_id = @CatalogItemId;";

        #endregion
    }
}
