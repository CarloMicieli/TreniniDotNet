using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug;
using TreniniDotNet.Application.Catalog.Railways.GetRailwaysList;
using TreniniDotNet.Web.Catalog.V1.Railways.Common.Requests;
using TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway;
using TreniniDotNet.Web.Catalog.V1.Railways.EditRailway;
using TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug;
using TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList;

namespace TreniniDotNet.Web.Catalog.V1.Railways
{
    public class RailwaysMapperProfile : Profile
    {
        public RailwaysMapperProfile()
        {
            CreateMap<CreateRailwayRequest, CreateRailwayInput>();
            CreateMap<EditRailwayRequest, EditRailwayInput>();
            CreateMap<GetRailwayBySlugRequest, GetRailwayBySlugInput>();
            CreateMap<GetRailwaysListRequest, GetRailwaysListInput>();
            CreateMap<TotalRailwayLengthRequest, TotalRailwayLengthInput>();
            CreateMap<RailwayGaugeRequest, RailwayGaugeInput>();
            CreateMap<PeriodOfActivityRequest, PeriodOfActivityInput>();
        }
    }
}