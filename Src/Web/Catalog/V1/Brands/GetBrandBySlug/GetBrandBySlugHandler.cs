using AutoMapper;
using TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandBySlug
{
    public class GetBrandBySlugHandler : UseCaseHandler<IGetBrandBySlugUseCase, GetBrandBySlugRequest, GetBrandBySlugInput>
    {
        public GetBrandBySlugHandler(IGetBrandBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
