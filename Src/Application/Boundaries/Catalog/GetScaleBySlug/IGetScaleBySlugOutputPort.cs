using TreniniDotNet.Application.Boundaries;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug
{
    public interface IGetScaleBySlugOutputPort : IOutputPortStandard<GetScaleBySlugOutput>
    {
        void ScaleNotFound(string message);
    }
}