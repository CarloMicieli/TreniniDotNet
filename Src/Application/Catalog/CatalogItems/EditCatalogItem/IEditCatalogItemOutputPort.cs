using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem
{
    public interface IEditCatalogItemOutputPort : IStandardOutputPort<EditCatalogItemOutput>
    {
        void CatalogItemNotFound(Slug catalogItem);

        void BrandNotFound(Slug brand);

        void ScaleNotFound(Slug scale);

        void RailwayNotFound(IEnumerable<Slug> railways);
    }
}
