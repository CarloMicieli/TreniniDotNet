using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Repositories.Catalog
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        public Task<CatalogItemId> Add(CatalogItem catalogItem)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogItem> GetBy(IBrand brand, ItemNumber itemNumber)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogItem> GetBy(Slug slug)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogItem> GetBy(CatalogItemId id)
        {
            throw new NotImplementedException();
        }
    }
}
