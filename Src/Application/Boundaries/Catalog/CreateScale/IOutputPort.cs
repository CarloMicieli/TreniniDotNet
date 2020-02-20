namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public interface IOutputPort : IOutputPortStandard<CreateScaleOutput>
    {
        void ScaleAlreadyExists(string message);
    }
}