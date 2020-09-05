using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public interface IGetRailwayBySlugOutputPort : IStandardOutputPort<GetRailwayBySlugOutput>
    {
        void RailwayNotFound(string message);
    }
}
