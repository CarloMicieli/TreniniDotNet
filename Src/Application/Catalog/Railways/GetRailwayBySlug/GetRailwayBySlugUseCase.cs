using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugUseCase : ValidatedUseCase<GetRailwayBySlugInput, IGetRailwayBySlugOutputPort>, IGetRailwayBySlugUseCase
    {
        private readonly RailwayService _railwayService;

        public GetRailwayBySlugUseCase(IGetRailwayBySlugOutputPort outputPort, RailwayService railwayService)
            : base(new GetRailwayBySlugInputValidator(), outputPort)
        {
            _railwayService = railwayService;
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
