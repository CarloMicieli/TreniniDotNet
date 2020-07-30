using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public interface ICreateCatalogItemOutputPort : IStandardOutputPort<CreateCatalogItemOutput>
    {
        void BrandNotFound(Slug brand);

        void CatalogItemAlreadyExists(Brand brand, ItemNumber itemNumber);

        void ScaleNotFound(Slug scale);

        void RailwayNotFound(IEnumerable<Slug> railways);
    }
}
