using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;

namespace TreniniDotNet.Web.UseCases.V1.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CreateBrandRequest, CreateBrandInput>();
            CreateMap<AddressRequest, AddressInput>();
        }
    }
}