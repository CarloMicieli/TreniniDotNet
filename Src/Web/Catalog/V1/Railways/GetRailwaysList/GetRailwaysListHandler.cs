using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways.GetRailwaysList;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList
{
    public class GetRailwaysListHandler : UseCaseHandler<IGetRailwaysListUseCase, GetRailwaysListRequest, GetRailwaysListInput>
    {
        public GetRailwaysListHandler(IGetRailwaysListUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
