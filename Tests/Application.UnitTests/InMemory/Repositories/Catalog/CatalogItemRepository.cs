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
            _context.CatalogItems.Add(catalogItem);
            return Task.FromResult(catalogItem.CatalogItemId);
        }

        public Task<bool> Exists(IBrand brand, ItemNumber itemNumber)
        {
            var exists = _context.CatalogItems
                .Any(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber);
            return Task.FromResult(exists);
        }

        public Task<ICatalogItem> GetBy(IBrand brand, ItemNumber itemNumber)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber)
                .FirstOrDefault();

            return Task.FromResult(catalogItem);
        }

        public Task<ICatalogItem> GetBy(Slug slug)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it.Slug == slug)
                .FirstOrDefault();

            return Task.FromResult(catalogItem);
        }
    }
}