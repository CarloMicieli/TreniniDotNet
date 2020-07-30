using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugUseCase : AbstractUseCase<GetRailwayBySlugInput, GetRailwayBySlugOutput, IGetRailwayBySlugOutputPort>
    {
        private readonly RailwaysService _railwayService;

        public GetRailwayBySlugUseCase(IUseCaseInputValidator<GetRailwayBySlugInput> inputValidator, IGetRailwayBySlugOutputPort outputPort, RailwaysService railwayService)
            : base(inputValidator, outputPort)
        {
            _railwayService = railwayService ?? throw new ArgumentNullException(nameof(railwayService));
        }

        protected override async Task Handle(GetRailwayBySlugInput input)
        {
            var railway = await _railwayService.GetBySlugAsync(input.Slug);
            if (railway is null)
            {
                OutputPort.RailwayNotFound($"The '{input.Slug}' railway was not found");
                return;
            }

            OutputPort.Standard(new GetRailwayBySlugOutput(railway));
        }
    }
}
