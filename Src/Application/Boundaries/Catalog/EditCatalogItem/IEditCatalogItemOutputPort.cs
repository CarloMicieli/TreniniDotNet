using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem
{
    public interface IEditCatalogItemOutputPort : IOutputPortStandard<EditCatalogItemOutput>
    {
        void CatalogItemNotFound(Slug slug);
    }
}
