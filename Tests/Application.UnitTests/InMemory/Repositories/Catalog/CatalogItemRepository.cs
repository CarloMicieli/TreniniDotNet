using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.InMemory.Repositories.Catalog
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly InMemoryContext _context;

        public CatalogItemRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<CatalogItemId> Add(CatalogItem catalogItem)
        {
            throw new System.NotImplementedException();
        }

        public Task<CatalogItem> GetBy(IBrand brand, ItemNumber itemNumber)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber)
                .FirstOrDefault();
            
            return Task.FromResult(catalogItem);
        }

        public Task<CatalogItem> GetBy(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public Task<CatalogItem> GetBy(CatalogItemId id)
        {
            throw new System.NotImplementedException();
        }
    }
}