using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugHandler : UseCaseHandler<IGetBrandBySlugUseCase, GetBrandBySlugRequest, GetBrandBySlugInput>
    {
        public GetBrandBySlugHandler(IGetBrandBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
