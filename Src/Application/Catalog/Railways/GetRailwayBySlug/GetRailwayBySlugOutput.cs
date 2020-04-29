using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public sealed class GetRailwayBySlugOutput : IUseCaseOutput
    {
        public GetRailwayBySlugOutput(IRailway railway)
        {
            Railway = railway;
        }

        public IRailway Railway { get; }
    }
}
