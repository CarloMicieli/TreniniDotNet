using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditScale
{
    public interface IEditScaleOutputPort : IOutputPortStandard<EditScaleOutput>
    {
        void ScaleNotFound(Slug slug);
    }
}
