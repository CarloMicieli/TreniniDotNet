using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public interface ICreateRailwayOutputPort : IStandardOutputPort<CreateRailwayOutput>
    {
        void RailwayAlreadyExists(Slug railway);
    }
}
