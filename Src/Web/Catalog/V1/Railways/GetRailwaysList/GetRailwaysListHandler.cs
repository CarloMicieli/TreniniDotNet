using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways.GetRailwaysList;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList
{
    public class GetRailwaysListHandler : UseCaseHandler<GetRailwaysListUseCase, GetRailwaysListRequest, GetRailwaysListInput>
    {
        public GetRailwaysListHandler(GetRailwaysListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
