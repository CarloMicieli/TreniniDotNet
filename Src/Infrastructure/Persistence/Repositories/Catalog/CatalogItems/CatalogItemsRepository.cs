using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems
{
    public sealed class CatalogItemsRepository : ICatalogItemsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogItemsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CatalogItemId> AddAsync(CatalogItem catalogItem)
        {
            var _ = await _unitOfWork.ExecuteAsync(InsertNewCatalogItem,
                ToCatalogItemParams(catalogItem));

            foreach (var rs in catalogItem.RollingStocks)
            {
                await InsertRollingStockAsync(catalogItem.Id, rs);
            }

            return catalogItem.Id;
        }

        public async Task UpdateAsync(CatalogItem catalogItem)
        {
            var _ = await _unitOfWork.ExecuteAsync(UpdateCatalogItemCommand,
                ToCatalogItemParams(catalogItem));

            var rollingStocksIds = await _unitOfWork.QueryAsync<Guid>(
                "SELECT rolling_stock_id FROM rolling_stocks WHERE catalog_item_id = @catalog_item_id",
                new { catalog_item_id = catalogItem.Id.ToGuid() });

            var ids = rollingStocksIds.ToList();

            foreach (var rs in catalogItem.RollingStocks)
            {
                if (ids.Contains(rs.Id))
                {
                    await UpdateRollingStockAsync(catalogItem.Id, rs);
                    ids.Remove(rs.Id);
                }
                else
                {
                    await InsertRollingStockAsync(catalogItem.Id, rs);
                }
            }

            foreach (var id in ids)
            {
                await RemoveRollingStockAsync(catalogItem.Id, new RollingStockId(id));
            }
        }

        private Task InsertRollingStockAsync(CatalogItemId catalogItemId, RollingStock rs)
        {
            var param = ToRollingStockParam(catalogItemId, rs);
            return _unitOfWork.ExecuteAsync(InsertNewRollingStockCommand, param);
        }

        private Task UpdateRollingStockAsync(CatalogItemId catalogItemId, RollingStock rs)
        {
            var param = ToRollingStockParam(catalogItemId, rs);
            return _unitOfWork.ExecuteAsync(UpdateRollingStockCommand, param);
        }

        private Task RemoveRollingStockAsync(CatalogItemId catalogItemId, RollingStockId rollingStockId)
        {
            return _unitOfWork.ExecuteAsync(DeleteRollingStockCommand, new
            {
                catalog_item_id = catalogItemId.ToGuid(),
                rolling_stock_id = rollingStockId.ToGuid()
            });
        }

        public async Task<bool> ExistsAsync(Brand brand, ItemNumber itemNumber)
        {
            var result = await _unitOfWork.ExecuteScalarAsync<string>(
                GetCatalogItemWithBrandAndItemNumberExistsQuery,
                new { brand_id = brand.Id.ToGuid(), item_number = itemNumber.Value });

            return string.IsNullOrEmpty(result) == false;
        }

        public async Task<CatalogItem?> GetByBrandAndItemNumberAsync(Brand brand, ItemNumber itemNumber)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemByBrandAndItemNumberQuery,
                new { brand_id = brand.Id.ToGuid(), item_number = itemNumber.Value });

            return FromCatalogItemDto(results);
        }

        public async Task<CatalogItem?> GetBySlugAsync(Slug slug)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemWithRelatedData>(
                GetCatalogItemBySlug,
                new { slug = slug.ToString() });

            return FromCatalogItemDto(results);
        }

        public async Task<PaginatedResult<CatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            var results = await _unitOfWork.QueryAsync<CatalogItemWithRelatedData>(
                GetLatestCatalogItemsQuery,
                new { skip = page.Start, limit = page.Limit + 1 });

            return new PaginatedResult<CatalogItem>(page, FromCatalogItemsDto(results));
        }

        public async Task DeleteAsync(CatalogItemId id)
        {
            await _unitOfWork.ExecuteAsync("DELETE FROM rolling_stocks WHERE catalog_item_id = @catalog_item_id",
                new {catalog_item_id = id.ToGuid()});
            
            await _unitOfWork.ExecuteAsync("DELETE FROM catalog_items WHERE catalog_item_id = @catalog_item_id",
                new {catalog_item_id = id.ToGuid()});
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

        private static CatalogItem ProjectToCatalogItem(
            IGrouping<CatalogItemGroupingKey, CatalogItemWithRelatedData> dto)
        {
            IReadOnlyList<RollingStock> rollingStocks = dto
                .Select(it => ProjectToRollingStock(it)!)
                .ToImmutableList();

            CatalogItemWithRelatedData it = dto.First();

            return new CatalogItem(
                new CatalogItemId(it.catalog_item_id),
                new BrandRef(new BrandId(it.brand_id), it.brand_slug, it.brand_name),
                new ItemNumber(it.item_number),
                new ScaleRef(new ScaleId(it.scale_id), it.scale_slug, it.scale_name, it.scale_ratio),
                EnumHelpers.RequiredValueFor<PowerMethod>(it.power_method),
                it.description,
                it.prototype_description,
                it.model_description,
                DeliveryDate.TryParse(it.delivery_date, out var dd) ? dd : null,
                it.available,
                rollingStocks,
                it.created_at.ToUtc(),
                it.modified_at.ToUtcOrDefault(),
                it.version ?? 1);
        }

        private static RollingStock? ProjectToRollingStock(CatalogItemWithRelatedData? values)
        {
            if (values is null)
            {
                return null;
            }

            var category = EnumHelpers.RequiredValueFor<Category>(values.category);

            var railway = new RailwayRef(
                new RailwayId(values.railway_id), values.railway_slug ?? "", values.railway_name ?? "");

            var id = new RollingStockId(values.rolling_stock_id);

            var epoch = Epoch.Parse(values.epoch);

            var lengthOverBuffer = LengthOverBuffer.CreateOrDefault(values.length_in, values.length_mm);
            var minRadius = MinRadius.CreateOrDefault(values.min_radius);

            var couplers = EnumHelpers.OptionalValueFor<Couplers>(values.couplers);

            if (Categories.IsLocomotive(category))
            {
                var prototype = Prototype.TryCreate(values.class_name, values.road_number, values.series);

                var dccInterface = EnumHelpers.RequiredValueFor<DccInterface>(values.dcc_interface!);
                var control = EnumHelpers.RequiredValueFor<Control>(values.control!);

                return new Locomotive(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    prototype,
                    couplers,
                    values.livery,
                    values.depot,
                    dccInterface,
                    control);
            }
            else if (Categories.IsPassengerCar(category))
            {
                var passengerCarType = EnumHelpers.OptionalValueFor<PassengerCarType>(values.passenger_car_type);
                var serviceLevel = values.service_level.ToServiceLevelOpt();

                return new PassengerCar(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    couplers,
                    values.type_name,
                    values.livery,
                    passengerCarType,
                    serviceLevel);
            }
            else if (Categories.IsFreightCar(category))
            {
                return new FreightCar(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    couplers,
                    values.type_name,
                    values.livery);
            }
            else
            {
                var dccInterface = EnumHelpers.RequiredValueFor<DccInterface>(values.dcc_interface!);
                var control = EnumHelpers.RequiredValueFor<Control>(values.control!);

                return new Train(
                    id,
                    railway,
                    category,
                    epoch,
                    lengthOverBuffer,
                    minRadius,
                    couplers,
                    values.type_name,
                    values.livery,
                    dccInterface,
                    control);
            }
        }

        private static CatalogItemParam ToCatalogItemParams(CatalogItem catalogItem) =>
            new CatalogItemParam
            {
                catalog_item_id = catalogItem.Id.ToGuid(),
                brand_id = catalogItem.Brand.Id.ToGuid(),
                scale_id = catalogItem.Scale.Id.ToGuid(),
                item_number = catalogItem.ItemNumber.Value,
                slug = catalogItem.Slug,
                power_method = catalogItem.PowerMethod.ToString(),
                delivery_date = catalogItem.DeliveryDate?.ToString(),
                available = catalogItem.IsAvailable,
                description = catalogItem.Description,
                model_description = catalogItem.ModelDescription,
                prototype_description = catalogItem.PrototypeDescription,
                last_modified = catalogItem.ModifiedDate?.ToDateTimeUtc(),
                version = catalogItem.Version
            };

        private static RollingStockParam ToRollingStockParam(CatalogItemId catalogItemId, RollingStock rs)
        {
            var values = new RollingStockParam
            {
                catalog_item_id = catalogItemId,
                rolling_stock_id = rs.Id.ToGuid(),
                epoch = rs.Epoch.ToString(),
                category = rs.Category.ToString(),
                railway_id = rs.Railway.Id.ToGuid(),
                length_mm = rs.Length?.Millimeters.Value,
                length_in = rs.Length?.Inches.Value,
                min_radius = rs.MinRadius?.Millimeters,
                couplers = rs.Couplers?.ToString(),
                livery = rs.Livery
            };

            return rs switch
            {
                Locomotive l => AppendLocomotiveValues(values, l),
                PassengerCar p => AppendPassengerCarValues(values, p),
                Train t => AppendTrainValues(values, t),
                FreightCar f => AppendFreightCarValues(values, f),
                _ => values
            };

            static RollingStockParam AppendLocomotiveValues(RollingStockParam param, Locomotive l)
            {
                param.depot = l.Depot;
                param.dcc_interface = l.DccInterface.ToString();
                param.control = l.Control.ToString();
                param.class_name = l.Prototype?.ClassName;
                param.road_number = l.Prototype?.RoadNumber;
                param.series = l.Prototype?.Series;
                return param;
            }

            static RollingStockParam AppendPassengerCarValues(RollingStockParam param, PassengerCar p)
            {
                param.passenger_car_type = p.PassengerCarType?.ToString();
                param.service_level = p.ServiceLevel?.ToString();
                param.type_name = p.TypeName;
                return param;
            }

            static RollingStockParam AppendTrainValues(RollingStockParam param, Train t)
            {
                param.dcc_interface = t.DccInterface.ToString();
                param.control = t.Control.ToString();
                param.type_name = t.TypeName;
                return param;
            }

            static RollingStockParam AppendFreightCarValues(RollingStockParam param, FreightCar f)
            {
                param.type_name = f.TypeName;
                return param;
            }
        }

        #region [ Query / Command text ]

        private const string GetLatestCatalogItemsQuery = @"SELECT 
                ci.catalog_item_id, ci.item_number, ci.slug, ci.power_method, ci.delivery_date, ci.available,
                ci.description, ci.model_description, ci.prototype_description, 
                rs.rolling_stock_id, rs.epoch, rs.category, rs.length_mm, rs.length_in, rs.min_radius,
                rs.class_name, rs.road_number, rs.series, rs.type_name, rs.couplers, rs.livery, rs.passenger_car_type, rs.service_level,
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
                rs.class_name, rs.road_number, rs.series, rs.type_name, rs.couplers, rs.livery, rs.passenger_car_type, rs.service_level,
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
            WHERE b.brand_id = @brand_id AND ci.item_number = @item_number;";

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

        private const string GetCatalogItemWithBrandAndItemNumberExistsQuery =
            @"SELECT slug FROM catalog_items WHERE brand_id = @brand_id AND item_number = @item_number LIMIT 1;";

        private const string InsertNewCatalogItem = @"INSERT INTO catalog_items(
	            catalog_item_id, brand_id, scale_id, item_number, slug, power_method, delivery_date, available,
                description, model_description, prototype_description, created, last_modified, version)
            VALUES(@catalog_item_id, @brand_id, @scale_id, @item_number, @slug, @power_method, @delivery_date, @available,
                @description, @model_description, @prototype_description, @created, @last_modified, @version);";

        private const string UpdateRollingStockCommand = @"UPDATE rolling_stocks SET 
	            epoch = @epoch, category = @category, railway_id = @railway_id, 
                length_mm = @length_mm, length_in = @length_in, min_radius = @min_radius,
                class_name = @class_name, road_number = @road_number, type_name = @type_name, 
                couplers = @couplers, livery = @livery, depot = @Depot,
                passenger_car_type = @passenger_car_type, service_level = @service_level, 
                dcc_interface = @dcc_interface, control = @control
            WHERE rolling_stock_id = @rolling_stock_id AND catalog_item_id = @catalog_item_id;";

        private const string DeleteRollingStockCommand =
            @"DELETE FROM rolling_stocks WHERE rolling_stock_id = @rolling_stock_id AND catalog_item_id = @catalog_item_id;";

        private const string InsertNewRollingStockCommand = @"INSERT INTO rolling_stocks(
	                rolling_stock_id, epoch, category, railway_id, catalog_item_id, length_mm, length_in, min_radius,
                    class_name, road_number, series, type_name, couplers, livery, depot, 
                    passenger_car_type, service_level, dcc_interface, control)
	        VALUES(@rolling_stock_id, @epoch, @category, @railway_id, @catalog_item_id, @length_mm, @length_in, @min_radius,
                    @class_name, @road_number, @series, @type_name, @couplers, @livery, @depot, 
	                @passenger_car_type, @service_level, @dcc_interface, @control);";

        private const string UpdateCatalogItemCommand = @"UPDATE catalog_items SET 
                brand_id = @brand_id, 
                scale_id = @scale_id, 
                item_number = @item_number, 
                slug = @slug, 
                power_method = @power_method,
                delivery_date = @delivery_date,
                available = @available,
                description = @description, 
                model_description = @model_description,
                prototype_description = @prototype_description,
                last_modified = @last_modified, 
                version = @version
            WHERE catalog_item_id = @catalog_item_id;";

        private const string DeleteAllRollingStocks =
            @"DELETE FROM rolling_stocks WHERE catalog_item_id = @catalog_item_id;";

        #endregion
    }
}