using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwayBySlug
{
    public class GetRailwayBySlugHandler : UseCaseHandler<IGetRailwayBySlugUseCase, GetRailwayBySlugRequest, GetRailwayBySlugInput>
    {
        public GetRailwayBySlugHandler(IGetRailwayBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
