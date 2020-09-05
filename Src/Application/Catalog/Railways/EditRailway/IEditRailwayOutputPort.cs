using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public interface IEditRailwayOutputPort : IStandardOutputPort<EditRailwayOutput>
    {
        void RailwayNotFound(Slug slug);
    }
}
