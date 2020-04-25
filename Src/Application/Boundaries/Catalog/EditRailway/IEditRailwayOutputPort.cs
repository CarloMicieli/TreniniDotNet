using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditRailway
{
    public interface IEditRailwayOutputPort : IOutputPortStandard<EditRailwayOutput>
    {
        void RailwayNotFound(Slug slug);
    }
}
