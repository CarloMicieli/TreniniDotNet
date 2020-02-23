using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugOutput : IUseCaseOutput
    {
        private readonly IRailway _railway;

        public GetRailwayBySlugOutput(IRailway railway)
        {
            _railway = railway;
        }

        public IRailway Railway => _railway;
    }
}
