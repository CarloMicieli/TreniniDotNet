using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugHandler : UseCaseHandler<IGetScaleBySlugUseCase, GetScaleBySlugRequest, GetScaleBySlugInput>
    {
        public GetScaleBySlugHandler(IGetScaleBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
