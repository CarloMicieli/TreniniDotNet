using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public interface ICreateCatalogItemOutputPort : IOutputPortStandard<CreateCatalogItemOutput>
    {
        void BrandNotFound(Slug brand);

        void CatalogItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber);

        void ScaleNotFound(Slug scale);

        void RailwayNotFound(IEnumerable<Slug> railways);
    }
}
