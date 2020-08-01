using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public sealed class CatalogItemsRepository : ICatalogItemsRepository
    {
        private readonly DapperUnitOfWork _unitOfWork;

        public CatalogItemsRepository(DapperUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CatalogItemId> AddAsync(CatalogItem catalogItem)
        {
            var _ = await _unitOfWork.ExecuteAsync(InsertNewCatalogItem, new
            {
                CatalogItemId = catalogItem.Id,
                BrandId = catalogItem.Brand.Id,
                ScaleId = catalogItem.Scale.Id,
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
                await InsertRollingStock(catalogItem.Id, rs);
            }
        
            return catalogItem.Id;
        }

        public async Task UpdateAsync(CatalogItem catalogItem)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateCatalogItemCommand, new
            {
                CatalogItemId = catalogItem.Id,
                BrandId = catalogItem.Brand.Id,
                ScaleId = catalogItem.Scale.Id,
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

            var rollingStocksIds = await _unitOfWork.QueryAsync<Guid>(
                "SELECT rolling_stock_id FROM rolling_stocks WHERE catalog_item_id = @Id", 
                new {Id = catalogItem.Id});

            var ids = rollingStocksIds.ToList();
        
            foreach (var rs in catalogItem.RollingStocks)
            {
                if (ids.Contains(rs.Id))
                {
                    await UpdateRollingStock(catalogItem.Id, rs);
                    ids.Remove(rs.Id);
                }
                else
                {
                    await InsertRollingStock(catalogItem.Id, rs);
                }
            }

            foreach (var id in ids)
            {
                await RemoveRollingStock(catalogItem.Id, new RollingStockId(id));
            }
        }

        private static object RollingStockToParam(RollingStock rs, CatalogItemId catalogItemId)
        {
            return rs switch
            {
                Locomotive l => new
                {
                    RollingStockId = l.Id,
                    Epoch = l.Epoch.ToString(),
                    l.Category,
                    RailwayId = l.Railway.Id,
                    CatalogItemId = catalogItemId,
                    LengthMm = l.Length?.Millimeters,
                    LengthIn = l.Length?.Inches,
                    MinRadius = l.MinRadius?.Millimeters,
                    l.Prototype?.ClassName,
                    l.Prototype?.RoadNumber,
                    l.Prototype?.Series,
                    Couplers = l.Couplers?.ToString(),
                    l.Livery,
                    PassengerCarType = (string?) null,
                    ServiceLevel = (string?) null,
                    DccInterface = l.DccInterface.ToString(),
                    Control = l.Control.ToString()
                },
                PassengerCar p => new
                {
                    RollingStockId = p.Id,
                    Epoch = p.Epoch.ToString(),
                    p.Category,
                    RailwayId = p.Railway.Id,
                    CatalogItemId = catalogItemId,
                    LengthMm = p.Length?.Millimeters,
                    LengthIn = p.Length?.Inches,
                    MinRadius = p.MinRadius?.Millimeters,
                    Couplers = p.Couplers?.ToString(),
                    p.Livery,
                    PassengerCarType = p.PassengerCarType?.ToString(),
                    ServiceLevel = p.ServiceLevel?.ToString(),
                    DccInterface = (string?) null,
                    Control = (string?) null
                },
                Train t => new
                {
                    RollingStockId = rs.Id,
                    Era = rs.Epoch.ToString(),
                    rs.Category,
                    RailwayId = rs.Railway.Id,
                    CatalogItemId = catalogItemId,
                    LengthMm = rs.Length?.Millimeters,
                    LengthIn = rs.Length?.Inches,
                    MinRadius = rs.MinRadius?.Millimeters,
                    t.TypeName,
                    Couplers = rs.Couplers?.ToString(),
                    rs.Livery,
                    PassengerCarType = (string?) null,
                    ServiceLevel = (string?) null,
                    DccInterface = t.DccInterface.ToString(),
                    Control = t.Control.ToString()
                },
                FreightCar f => new
                {
                    RollingStockId = rs.Id,
                    Era = rs.Epoch.ToString(),
                    rs.Category,
                    RailwayId = rs.Railway.Id,
                    CatalogItemId = catalogItemId,
                    LengthMm = rs.Length?.Millimeters,
                    LengthIn = rs.Length?.Inches,
                    MinRadius = rs.MinRadius?.Millimeters,
                    f.TypeName,
                    Couplers = rs.Couplers?.ToString(),
                    rs.Livery,
                    PassengerCarType = (string?) null,
                    ServiceLevel = (string?) null,
                    DccInterface = (string?) null,
                    Control = (string?) null
                },
                _ => throw new InvalidOperationException("")
            };
        }
        
        private Task InsertRollingStock(CatalogItemId catalogItemId, RollingStock rs)
        {
            var param = RollingStockToParam(rs, catalogItemId);
            return _unitOfWork.ExecuteAsync(InsertNewRollingStockCommand, param);
        }
        
        private Task UpdateRollingStock(CatalogItemId catalogItemId, RollingStock rs)
        {
            //     var _ = await connection.ExecuteAsync(UpdateRollingStockCommand, new
            //     {
            //         RollingStockId = rollingStock.Id,
            //         Era = rollingStock.Epoch.ToString(),
            //         rollingStock.Category,
            //         RailwayId = rollingStock.Railway.Id,
            //         CatalogItemId = catalogItem.Id,
            //         LengthMm = rollingStock.Length?.Millimeters,
            //         LengthIn = rollingStock.Length?.Inches,
            //         MinRadius = rollingStock.MinRadius?.Millimeters,
            //         rollingStock.Prototype?.ClassName,
            //         rollingStock.Prototype?.RoadNumber,
            //         rollingStock.Prototype?.TypeName,
            //         Couplers = rollingStock.Couplers?.ToString(),
            //         rollingStock.Livery,
            //         PassengerCarType = rollingStock.PassengerCarType?.ToString(),
            //         ServiceLevel = rollingStock.ServiceLevel?.ToString(),
            //         DccInterface = rollingStock.DccInterface.ToString(),
            //         Control = rollingStock.Control.ToString()
            //     });
            throw new NotImplementedException();
        }
        
        private Task RemoveRollingStock(CatalogItemId catalogItemId, RollingStockId rollingStockId)
        {
            return _unitOfWork.ExecuteAsync(DeleteRollingStockCommand, new
            {
                catalogItemId,
                rollingStockId
            });
        }

        public async Task<bool> ExistsAsync(Brand brand, ItemNumber itemNumber)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetCatalogItemWithBrandAndItemNumberExistsQuery,
                new { @brandId = brand.Id, @itemNumber = itemNumber.Value });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<CatalogItem?> GetByBrandAndItemNumberAsync(Brand brand, ItemNumber itemNumber)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemByBrandAndItemNumberQuery,
                new { @brandId = brand.Id, @itemNumber = itemNumber.Value });

            return FromCatalogItemDto(results);
        }

        public async Task<CatalogItem?> GetBySlugAsync(Slug slug)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemBySlug,
                new { @slug = slug.ToString() });

            return FromCatalogItemDto(results);
        }

        public async Task<PaginatedResult<CatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemWithRelatedData>(
                GetLatestCatalogItemsQuery,
                new { @skip = page.Start, @limit = page.Limit + 1 });

            return new PaginatedResult<CatalogItem>(page, FromCatalogItemsDto(results));
        }

        public Task<PaginatedResult<CatalogItem>> GetAllAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CatalogItemId id)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<CatalogItem> FromCatalogItemsDto(IEnumerable<CatalogItemWithRelatedData> results)
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
                .Select(ProjectToCatalogItem)
                .ToList();
        }

        private CatalogItem? FromCatalogItemDto(IEnumerable<CatalogItemWithRelatedData> results)
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
                    .Select(ProjectToCatalogItem)
                    .FirstOrDefault();
                return catalogItem;
            }
            else
            {
                return null;
            }
        }

        private CatalogItem ProjectToCatalogItem(IGrouping<CatalogItemGroupingKey, CatalogItemWithRelatedData> dto)
        {
            throw new NotImplementedException();
            /*
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
                it.version ?? 1);*/
        }

        // private IRollingStock? ProjectToRollingStock(CatalogItemWithRelatedData? dto)
        // {
        //     if (dto is null)
        //     {
        //         return null;
        //     }
        //     else
        //     {
        //         var railwayInfo = ProjectToRailwayInfo(dto!);
        //         return RollingStock(dto!, railwayInfo);
        //     }
        // }

        // private static IRailwayInfo ProjectToRailwayInfo(CatalogItemWithRelatedData dto) =>
        //     new RailwayInfo(dto.railway_id, dto.railway_slug, dto.railway_name);
        //
        // private IRollingStock RollingStock(CatalogItemWithRelatedData dto, IRailwayInfo railway) =>
        //     _rsFactory.RollingStockWith(
        //         dto.rolling_stock_id,
        //         railway,
        //         dto.era,
        //         dto.category,
        //         dto.length_mm,
        //         dto.length_in,
        //         dto.min_radius,
        //         dto.class_name,
        //         dto.road_number,
        //         dto.type_name,
        //         dto.couplers,
        //         dto.livery,
        //         dto.passenger_car_type,
        //         dto.service_level,
        //         dto.dcc_interface,
        //         dto.control
        //     );

        #region [ Query / Command text ]

        private const string GetLatestCatalogItemsQuery = @"SELECT 
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, ci.available,
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.epoch, rs.category, rs.length_mm, rs.length_in, rs.min_radius,
                rs.class_name, rs.road_number, rs.type_name, rs.couplers, rs.livery, rs.passenger_car_type, rs.service_level,
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
                rs.rolling_stock_id, rs.epoch, rs.category, rs.length_mm, rs.length_in, rs.min_radius,
                rs.class_name, rs.road_number, rs.type_name, rs.couplers, rs.livery, rs.passenger_car_type, rs.service_level,
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
                rs.rolling_stock_id, rs.epoch, rs.category, rs.length_mm, rs.length_in, rs.min_radius,
                rs.class_name, rs.road_number, rs.type_name, rs.couplers, rs.livery, rs.passenger_car_type, rs.service_level,
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

        private const string UpdateRollingStockCommand = @"UPDATE rolling_stocks SET 
	            epoch = @Epoch, category = @Category, railway_id = @RailwayId, length_mm = @LengthMm, length_in = @LengthIn, min_radius = @MinRadius,
                class_name = @ClassName, road_number = @RoadNumber, type_name = @TypeName, couplers = @Couplers, livery = @Livery,
                passenger_car_type = @PassengerCarType, service_level = @ServiceLevel, 
                dcc_interface = @DccInterface, control = @Control
            WHERE rolling_stock_id = @RollingStockId AND catalog_item_id = @CatalogItemId;";

        private const string DeleteRollingStockCommand = @"DELETE FROM rolling_stocks WHERE rolling_stock_id = @RollingStockId AND catalog_item_id = @CatalogItemId;";

        private const string InsertNewRollingStockCommand = @"INSERT INTO rolling_stocks(
	            rolling_stock_id, epoch, category, railway_id, catalog_item_id, length_mm, length_in, min_radius,
                class_name, road_number, type_name, couplers, livery, passenger_car_type, service_level, dcc_interface, control)
	        VALUES(@RollingStockId, @Epoch, @Category, @RailwayId, @CatalogItemId, @LengthMm, @LengthIn, @MinRadius,
                @ClassName, @RoadNumber, @TypeName, @Couplers, @Livery, @PassengerCarType, @ServiceLevel, @DccInterface, @Control);";

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
