namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public interface ICreateRailwayOutputPort : IOutputPortStandard<CreateRailwayOutput>
    {
        void RailwayAlreadyExists(string message);
    }
}
