using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public interface ICreateScaleOutputPort : IOutputPortStandard<CreateScaleOutput>
    {
        void ScaleAlreadyExists(string message);
    }
}