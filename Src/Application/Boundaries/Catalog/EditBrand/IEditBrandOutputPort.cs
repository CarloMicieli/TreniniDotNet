using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditBrand
{
    public interface IEditBrandOutputPort : IOutputPortStandard<EditBrandOutput>
    {
        void BrandNotFound(Slug brandSlug);
    }
}
