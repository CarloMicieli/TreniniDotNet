using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugOutput : IUseCaseOutput
    {
        public GetRailwayBySlugOutput(Railway railway)
        {
            Railway = railway;
        }

        public Railway Railway { get; }
    }
}
