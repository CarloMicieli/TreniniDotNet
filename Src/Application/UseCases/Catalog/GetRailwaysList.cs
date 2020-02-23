using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetRailwaysList : IGetRailwaysListUseCase
    {
        private readonly IGetRailwaysListOutputPort _outputPort;
        private readonly RailwayService _railwayService;

        public GetRailwaysList(IGetRailwaysListOutputPort outputPort, RailwayService railwayService)
        {
            _outputPort = outputPort;
            _railwayService = railwayService;
        }

        public async Task Execute(GetRailwaysListInput input)
        {
            var railways = await _railwayService.GetAll();
            OutputPort.Standard(new GetRailwaysListOutput(railways));
        }

        private IGetRailwaysListOutputPort OutputPort => _outputPort;
    }    
}
