using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetRailwayBySlug : ValidatedUseCase<GetRailwayBySlugInput, IGetRailwayBySlugOutputPort>, IGetRailwayBySlugUseCase
    {
        private readonly IGetRailwayBySlugOutputPort _outputPort;
        private readonly RailwayService _railwayService;

        public GetRailwayBySlug(IUseCaseInputValidator<GetRailwayBySlugInput> validator, IGetRailwayBySlugOutputPort outputPort, RailwayService railwayService)
            : base(validator, outputPort)
        {
            _outputPort = outputPort;
            _railwayService = railwayService;
        }

        protected override async Task Handle(GetRailwayBySlugInput input)
        {
            var railway = await _railwayService.GetBy(input.Slug);
            if (railway is null)
            {
                _outputPort.RailwayNotFound($"The '{input.Slug}' railway was not found");
                return;
            }

            _outputPort.Standard(new GetRailwayBySlugOutput(railway));
        }
    }
}
