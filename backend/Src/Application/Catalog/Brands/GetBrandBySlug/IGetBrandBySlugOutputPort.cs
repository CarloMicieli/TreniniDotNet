using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public interface IGetBrandBySlugOutputPort : IStandardOutputPort<GetBrandBySlugOutput>
    {
        void BrandNotFound(string message);
    }
}
