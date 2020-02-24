using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog
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
            var scales = await _scaleService.GetAll();
            OutputPort.Standard(new GetScalesListOutput(scales));
        }

        private IGetScalesListOutputPort OutputPort => _outputPort;
    }
}
