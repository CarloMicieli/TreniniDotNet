using AutoMapper;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Web.Collecting.V1.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Common
{
    public class CommonMapperProfile : Profile
    {
        public CommonMapperProfile()
        {
            CreateMap<PriceRequest, PriceInput>();
        }
    }
}