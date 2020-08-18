using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScalesList
{
    public sealed class GetScalesListUseCase : AbstractUseCase<GetScalesListInput, GetScalesListOutput, IGetScalesListOutputPort>
    {
        private readonly ScalesService _scalesService;

        public GetScalesListUseCase(
            IUseCaseInputValidator<GetScalesListInput> inputValidator,
            IGetScalesListOutputPort outputPort,
            ScalesService scalesService)
            : base(inputValidator, outputPort)
        {
            _scalesService = scalesService ?? throw new ArgumentNullException(nameof(scalesService));
        }

        protected override async Task Handle(GetScalesListInput input)
        {
            var paginatedResult = await _scalesService.FindAllScales(input.Page);
            OutputPort.Standard(new GetScalesListOutput(paginatedResult));
        }
    }
}
