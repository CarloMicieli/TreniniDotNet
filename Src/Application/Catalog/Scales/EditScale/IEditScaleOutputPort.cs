using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public interface IEditScaleOutputPort : IStandardOutputPort<EditScaleOutput>
    {
        void ScaleNotFound(Slug slug);
    }
}
