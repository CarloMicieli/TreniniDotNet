namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public interface ICreateScaleOutputPort : IOutputPortStandard<CreateScaleOutput>
    {
        void ScaleAlreadyExists(string message);
    }
}