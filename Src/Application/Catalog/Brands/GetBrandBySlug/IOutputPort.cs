using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public interface IGetBrandBySlugOutputPort : IOutputPortStandard<GetBrandBySlugOutput>
    {
        void BrandNotFound(string message);
    }
}
