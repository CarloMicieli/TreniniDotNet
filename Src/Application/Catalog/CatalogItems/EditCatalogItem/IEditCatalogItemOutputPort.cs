using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public interface IEditCatalogItemOutputPort : IOutputPortStandard<EditCatalogItemOutput>
    {
        void CatalogItemNotFound(Slug catalogItem);

        void BrandNotFound(Slug brand);

        void ScaleNotFound(Slug scale);

        void RailwayNotFound(IEnumerable<Slug> railways);
    }
}
