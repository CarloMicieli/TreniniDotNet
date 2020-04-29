using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug
{
    public class GetScaleBySlugHandler : UseCaseHandler<IGetScaleBySlugUseCase, GetScaleBySlugRequest, GetScaleBySlugInput>
    {
        public GetScaleBySlugHandler(IGetScaleBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
