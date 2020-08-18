using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug
{
    public class GetScaleBySlugHandler : UseCaseHandler<GetScaleBySlugUseCase, GetScaleBySlugRequest, GetScaleBySlugInput>
    {
        public GetScaleBySlugHandler(GetScaleBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
