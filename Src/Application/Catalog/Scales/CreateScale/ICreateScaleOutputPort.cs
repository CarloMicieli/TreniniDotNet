using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public interface ICreateScaleOutputPort : IStandardOutputPort<CreateScaleOutput>
    {
        void ScaleAlreadyExists(Slug scaleSlug);
    }
}
