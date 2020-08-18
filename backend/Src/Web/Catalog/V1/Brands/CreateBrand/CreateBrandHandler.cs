using AutoMapper;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.CreateBrand
{
    public class CreateBrandHandler : UseCaseHandler<CreateBrandUseCase, CreateBrandRequest, CreateBrandInput>
    {
        public CreateBrandHandler(CreateBrandUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
