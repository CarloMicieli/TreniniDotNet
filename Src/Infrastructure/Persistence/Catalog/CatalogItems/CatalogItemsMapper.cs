using System;
using System.Linq;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Scales;
using LanguageExt;
using static LanguageExt.Prelude;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;

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

                return result.IfFail(() => default(IRollingStock));
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