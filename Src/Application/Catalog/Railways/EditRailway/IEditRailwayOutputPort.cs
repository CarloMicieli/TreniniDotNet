using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public interface IEditRailwayOutputPort : IOutputPortStandard<EditRailwayOutput>
    {
        void RailwayNotFound(Slug slug);
    }
}
