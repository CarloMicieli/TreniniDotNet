using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public interface IGetScaleBySlugOutputPort : IOutputPortStandard<GetScaleBySlugOutput>
    {
        void ScaleNotFound(string message);
    }
}