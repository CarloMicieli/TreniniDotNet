using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public interface IEditScaleOutputPort : IOutputPortStandard<EditScaleOutput>
    {
        void ScaleNotFound(Slug slug);
    }
}
