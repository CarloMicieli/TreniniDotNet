using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public interface IEditBrandOutputPort : IOutputPortStandard<EditBrandOutput>
    {
        void BrandNotFound(Slug brandSlug);
    }
}
