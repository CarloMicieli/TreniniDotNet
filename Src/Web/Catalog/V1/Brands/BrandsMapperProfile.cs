using AutoMapper;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug;
using TreniniDotNet.Application.Catalog.Brands.GetBrandsList;
using TreniniDotNet.Web.Catalog.V1.Brands.Common.Requests;
using TreniniDotNet.Web.Catalog.V1.Brands.CreateBrand;
using TreniniDotNet.Web.Catalog.V1.Brands.EditBrand;
using TreniniDotNet.Web.Catalog.V1.Brands.GetBrandBySlug;
using TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList;

namespace TreniniDotNet.Web.Catalog.V1.Brands
{
    public class BrandsMapperProfile : Profile
    {
        public BrandsMapperProfile()
        {
            CreateMap<CreateBrandRequest, CreateBrandInput>();
            CreateMap<EditBrandRequest, EditBrandInput>();
            CreateMap<GetBrandBySlugRequest, GetBrandBySlugInput>();
            CreateMap<GetBrandsListRequest, GetBrandsListInput>();
            CreateMap<AddressRequest, AddressInput>();
        }
    }
}