using System.Collections.Generic;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem
{
    public interface IEditCatalogItemOutputPort : IOutputPortStandard<EditCatalogItemOutput>
    {
        void CatalogItemNotFound(Slug catalogItem);

        void BrandNotFound(Slug brand);

        void ScaleNotFound(Slug scale);

        void RailwayNotFound(IEnumerable<Slug> railways);
    }
}
