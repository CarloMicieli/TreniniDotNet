using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog.Scales
{
    public sealed class GetScalesList : IGetScalesListUseCase
    {
        private readonly IGetScalesListOutputPort _outputPort;
        private readonly ScaleService _scaleService;

        public GetScalesList(IGetScalesListOutputPort outputPort, ScaleService scaleService)
        {
            _outputPort = outputPort;
            _scaleService = scaleService;
        }

        public async Task Execute(GetScalesListInput input)
        {
            var paginatedResult = await _scaleService.FindAllScales(input.Page);
            OutputPort.Standard(new GetScalesListOutput(paginatedResult));
        }

        private IGetScalesListOutputPort OutputPort => _outputPort;
    }
}
