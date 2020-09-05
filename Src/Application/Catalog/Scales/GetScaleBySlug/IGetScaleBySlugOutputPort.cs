using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public interface IGetScaleBySlugOutputPort : IStandardOutputPort<GetScaleBySlugOutput>
    {
        void ScaleNotFound(string message);
    }
}
