using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public interface ICreateRailwayOutputPort : IOutputPortStandard<CreateRailwayOutput>
    {
        void RailwayAlreadyExists(Slug railway);
    }
}
