using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public interface ICreateCatalogItemOutputPort : IOutputPortStandard<CreateCatalogItemOutput>
    {
        void BrandNotFound(Slug brand);

        void CatalogItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber);

        void ScaleNotFound(Slug scale);

        void RailwayNotFound(IEnumerable<Slug> railways);
    }
}
