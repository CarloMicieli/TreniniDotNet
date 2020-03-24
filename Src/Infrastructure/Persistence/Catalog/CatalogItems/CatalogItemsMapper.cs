using System;
using System.Linq;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Scales;
using LanguageExt;
using static LanguageExt.Prelude;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    internal sealed class CatalogItemsMapper
    {
        private readonly ICatalogItemsFactory _factory;
        private readonly IRollingStocksFactory _rsFactory;

        public CatalogItemsMapper(ICatalogItemsFactory factory, IRollingStocksFactory rsFactory)
        {
            _factory = factory ??
                throw new ArgumentNullException(nameof(factory));

            _rsFactory = rsFactory ??
                throw new ArgumentNullException(nameof(rsFactory));
        }

        public IBrandInfo? ProjectToBrandInfo(CatalogItemWithRelatedData? dto) =>
            (dto != null) ? new BrandInfo(dto.brand_id, dto.brand_slug, dto.brand_name) : null;

        public IScaleInfo? ProjectToScaleInfo(CatalogItemWithRelatedData? dto) =>
            (dto != null) ? new ScaleInfo(dto.scale_id, dto.scale_slug, dto.scale_name, dto.scale_ratio) : null;

        public ICatalogItem? ProjectToCatalogItem(IGrouping<CatalogItemGroupingKey, CatalogItemWithRelatedData> dto)
        {
            IReadOnlyList<IRollingStock> rollingStocks = dto
                .Select(it => this.ProjectToRollingStock(it)!)
                .ToImmutableList();

            CatalogItemWithRelatedData it = dto.First();

            return _factory.HydrateCatalogItem(
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
                it.version ?? 1).IfFail(() => default);
        }

        public IRollingStock? ProjectToRollingStock(CatalogItemWithRelatedData? dto)
        {
            if (dto is null)
            {
                return null;
            }
            else
            {
                var result =
                (
                    from railway in RailwayInfo(dto!)
                    from rs in RollingStock(dto!, railway)
                    select rs
                );

                return result.IfFail(() => default);
            }
        }

        private Validation<Error, IRollingStock> RollingStock(CatalogItemWithRelatedData dto, IRailwayInfo railway) =>
            _rsFactory.HydrateRollingStock(
                dto.rolling_stock_id,
                railway,
                dto.era,
                dto.category,
                dto.length,
                dto.class_name,
                dto.road_number,
                dto.type_name,
                dto.dcc_interface,
                dto.control
            );

        private static Validation<Error, IRailwayInfo> RailwayInfo(CatalogItemWithRelatedData dto) =>
            Success<Error, IRailwayInfo>(new RailwayInfo(dto.railway_id, dto.railway_slug, dto.railway_name, dto.railway_country));

    }
}