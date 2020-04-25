using AutoMapper;
using TreniniDotNet.Application.Boundaries.Common;

namespace TreniniDotNet.Web.UseCases.V1.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddressRequest, AddressInput>();
        }
    }
}
