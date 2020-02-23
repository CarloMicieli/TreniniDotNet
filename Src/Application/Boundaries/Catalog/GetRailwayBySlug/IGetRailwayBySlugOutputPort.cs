namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug
{
    public interface IGetRailwayBySlugOutputPort : IOutputPortStandard<GetRailwayBySlugOutput>
    {
        void RailwayNotFound(string message);
    }
}
