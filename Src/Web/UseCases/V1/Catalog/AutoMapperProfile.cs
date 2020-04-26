using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;
using TreniniDotNet.Application.Boundaries.Catalog.EditScale;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale;
using TreniniDotNet.Web.UseCases.V1.Catalog.EditBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.EditCatalogItem;
using TreniniDotNet.Web.UseCases.V1.Catalog.EditRailway;
using TreniniDotNet.Web.UseCases.V1.Catalog.EditScale;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwayBySlug;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBrandRequest, CreateBrandInput>();
            CreateMap<EditBrandRequest, EditBrandInput>();
            CreateMap<GetBrandBySlugRequest, GetBrandBySlugInput>();
            CreateMap<GetBrandsListRequest, GetBrandsListInput>();

            CreateMap<CreateRailwayRequest, CreateRailwayInput>();
            CreateMap<EditRailwayRequest, EditRailwayInput>();
            CreateMap<GetRailwayBySlugRequest, GetRailwayBySlugInput>();
            CreateMap<GetRailwaysListRequest, GetRailwaysListInput>();
            CreateMap<TotalRailwayLengthRequest, TotalRailwayLengthInput>();
            CreateMap<RailwayGaugeRequest, RailwayGaugeInput>();
            CreateMap<PeriodOfActivityRequest, PeriodOfActivityInput>();

            CreateMap<CreateScaleRequest, CreateScaleInput>();
            CreateMap<EditScaleRequest, EditScaleInput>();
            CreateMap<GetScaleBySlugRequest, GetScaleBySlugInput>();
            CreateMap<GetScalesListRequest, GetScalesListInput>();
            CreateMap<ScaleGaugeRequest, ScaleGaugeInput>();

            CreateMap<CreateCatalogItemRequest, CreateCatalogItemInput>();
            CreateMap<EditCatalogItemRequest, EditCatalogItemInput>();
            CreateMap<GetCatalogItemBySlugRequest, GetCatalogItemBySlugInput>();
            CreateMap<RollingStockRequest, RollingStockInput>();
            CreateMap<LengthOverBufferRequest, LengthOverBufferInput>();
        }
    }
}