using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public sealed class GetRailwaysListUseCase : IGetRailwaysListUseCase
    {
        private readonly IGetRailwaysListOutputPort _outputPort;
        private readonly RailwayService _railwayService;

        public GetRailwaysListUseCase(IGetRailwaysListOutputPort outputPort, RailwayService railwayService)
        {
            _outputPort = outputPort;
            _railwayService = railwayService;
        }

        public async Task Execute(GetRailwaysListInput input)
        {
            var paginatedResult = await _railwayService.FindAllRailways(input.Page);
            OutputPort.Standard(new GetRailwaysListOutput(paginatedResult));
        }

        private IGetRailwaysListOutputPort OutputPort => _outputPort;
    }
}
