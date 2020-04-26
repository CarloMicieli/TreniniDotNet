using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList
{
    public class GetRailwaysListHandler : UseCaseHandler<IGetRailwaysListUseCase, GetRailwaysListRequest, GetRailwaysListInput>
    {
        public GetRailwaysListHandler(IGetRailwaysListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
