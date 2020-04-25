using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog.Railways
{
    public sealed class GetRailwayBySlug : ValidatedUseCase<GetRailwayBySlugInput, IGetRailwayBySlugOutputPort>, IGetRailwayBySlugUseCase
    {
        private readonly RailwayService _railwayService;

        public GetRailwayBySlug(IGetRailwayBySlugOutputPort outputPort, RailwayService railwayService)
            : base(new GetRailwayBySlugInputValidator(), outputPort)
        {
            _railwayService = railwayService;
        }

        protected override async Task Handle(GetRailwayBySlugInput input)
        {
            var railway = await _railwayService.GetBy(input.Slug);
            if (railway is null)
            {
                OutputPort.RailwayNotFound($"The '{input.Slug}' railway was not found");
                return;
            }

            OutputPort.Standard(new GetRailwayBySlugOutput(railway));
        }
    }
}
