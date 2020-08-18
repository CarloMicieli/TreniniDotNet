using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public interface IEditBrandOutputPort : IStandardOutputPort<EditBrandOutput>
    {
        void BrandNotFound(Slug brandSlug);
    }
}
