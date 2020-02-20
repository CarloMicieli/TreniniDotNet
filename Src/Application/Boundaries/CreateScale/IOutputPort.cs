namespace TreniniDotNet.Application.Boundaries.CreateScale
{
    public interface IOutputPort : IOutputPortStandard<CreateScaleOutput>
    {
        void ScaleAlreadyExists(string message);
    }
}