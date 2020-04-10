using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway;

namespace TreniniDotNet.Web.UseCases.V1.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CreateBrandRequest, CreateBrandInput>();
            CreateMap<AddressRequest, AddressInput>();

            CreateMap<CreateRailwayRequest, CreateRailwayInput>();
            CreateMap<TotalRailwayLengthRequest, TotalRailwayLengthInput>();
            CreateMap<RailwayGaugeRequest, RailwayGaugeInput>();
            CreateMap<PeriodOfActivityRequest, PeriodOfActivityInput>();
        }
    }
}