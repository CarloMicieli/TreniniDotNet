using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug;
using TreniniDotNet.Application.Catalog.Scales.GetScalesList;
using TreniniDotNet.Web.Catalog.V1.Scales.CreateScale;
using TreniniDotNet.Web.Catalog.V1.Scales.EditScale;
using TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug;
using TreniniDotNet.Web.Catalog.V1.Scales.GetScalesList;

namespace TreniniDotNet.Web.Catalog.V1.Scales.Common.Requests
{
    public class ScalesMapperProfile : Profile
    {
        public ScalesMapperProfile()
        {
            CreateMap<CreateScaleRequest, CreateScaleInput>();
            CreateMap<EditScaleRequest, EditScaleInput>();
            CreateMap<GetScaleBySlugRequest, GetScaleBySlugInput>();
            CreateMap<GetScalesListRequest, GetScalesListInput>();
            CreateMap<ScaleGaugeRequest, ScaleGaugeInput>();
        }
    }
}