using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwaysList
{
    public sealed class GetRailwaysListUseCase : AbstractUseCase<GetRailwaysListInput, GetRailwaysListOutput, IGetRailwaysListOutputPort>
    {
        private readonly RailwaysService _railwaysService;

        public GetRailwaysListUseCase(
            IUseCaseInputValidator<GetRailwaysListInput> inputValidator,
            IGetRailwaysListOutputPort outputPort,
            RailwaysService railwaysService)
            : base(inputValidator, outputPort)
        {
            _railwaysService = railwaysService ?? throw new ArgumentNullException(nameof(railwaysService));
        }

        protected override async Task Handle(GetRailwaysListInput input)
        {
            var paginatedResult = await _railwaysService.FindAllRailways(input.Page);
            OutputPort.Standard(new GetRailwaysListOutput(paginatedResult));
        }
    }
}
