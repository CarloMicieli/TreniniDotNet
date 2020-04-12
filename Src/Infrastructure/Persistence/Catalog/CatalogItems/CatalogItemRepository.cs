using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
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

        public CatalogItemRepository(IDatabaseContext dbContext, ICatalogItemsFactory factory)
        {
            _dbContext = dbContext;
            _factory = factory;
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
                    rs.Era,
                    rs.Category,
                    rs.Railway.RailwayId,
                    catalogItem.CatalogItemId,
                    LengthMm = rs.Length?.Millimeters,
                    LengthIn = rs.Length?.Inches,
                    rs.ClassName,
                    rs.RoadNumber,
                    rs.TypeName,
                    DccInterface = rs.DccInterface.ToString(),
                    Control = rs.Control.ToString()
                });
            }

            return catalogItem.CatalogItemId;
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

        public async Task<ICatalogItem?> GetByAsync(IBrandInfo brand, ItemNumber itemNumber)
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

        private ICatalogItem? ProjectToCatalogItem(IGrouping<CatalogItemGroupingKey, CatalogItemWithRelatedData> dto)
        {
            IReadOnlyList<IRollingStock> rollingStocks = dto
                .Select(it => this.ProjectToRollingStock(it)!)
                .ToImmutableList();

            CatalogItemWithRelatedData it = dto.First();

            return _factory.NewCatalogItem(
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
            new RailwayInfo(dto.railway_id, dto.railway_slug, dto.railway_name, dto.railway_country!); //TODO: fixme

        private IRollingStock RollingStock(CatalogItemWithRelatedData dto, IRailwayInfo railway) =>
            _factory.NewRollingStock(
                dto.rolling_stock_id,
                railway,
                dto.era,
                dto.category,
                LengthOverBuffer.CreateOrDefault(dto.length_in, dto.length_mm),
                dto.class_name,
                dto.road_number,
                dto.type_name,
                dto.dcc_interface,
                dto.control
            );

        #region [ Query / Command text ]

        private const string GetCatalogItemByBrandAndItemNumberQuery = @"SELECT 
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, 
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length_mm, rs.length_in, rs.class_name, rs.road_number,
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
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, 
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.era, rs.category, rs.length_mm, rs.length_in, rs.class_name, rs.road_number,
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
                class_name, road_number, type_name, dcc_interface, control)
	        VALUES(@RollingStockId, @Era, @Category, @RailwayId, @CatalogItemId, @LengthMm, @LengthIn, 
                @ClassName, @RoadNumber, @TypeName, @DccInterface, @Control);";

        #endregion
    }
}
