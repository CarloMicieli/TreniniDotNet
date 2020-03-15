using System.Collections.Generic;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public interface ICreateCatalogItemOutputPort : IOutputPortStandard<CreateCatalogItemOutput>
    {
        void BrandNameNotFound(string message);

        void CatalogItemAlreadyExists(string message);

        void ScaleNotFound(string message);

        void RailwayNotFound(string message, IEnumerable<string> railwayNames);
    }
}
