using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug
{
    public interface IGetRailwayBySlugOutputPort : IOutputPortStandard<GetRailwayBySlugOutput>
    {
        void RailwayNotFound(string message);
    }
}
