using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly InMemoryContext _context;

        public CatalogItemRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<CatalogItemId> AddAsync(ICatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);
            return Task.FromResult(catalogItem.CatalogItemId);
        }

        public Task<bool> ExistsAsync(IBrandInfo brand, ItemNumber itemNumber)
        {
            var exists = _context.CatalogItems
                .Any(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber);
            return Task.FromResult(exists);
        }

        public Task<ICatalogItem> GetByBrandAndItemNumberAsync(IBrandInfo brand, ItemNumber itemNumber)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber)
                .FirstOrDefault();

            return Task.FromResult(catalogItem);
        }

        public Task<ICatalogItem> GetBySlugAsync(Slug slug)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it.Slug == slug)
                .FirstOrDefault();

            return Task.FromResult(catalogItem);
        }

        public Task UpdateAsync(ICatalogItem catalogItem) =>
            Task.CompletedTask;

        public Task<PaginatedResult<ICatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            var results = _context.CatalogItems.OrderByDescending(it => it.CreatedDate)
                .Take(page.Limit)
                .Skip(page.Start);
            return Task.FromResult(new PaginatedResult<ICatalogItem>(page, results));
        }

        public Task AddRollingStockAsync(ICatalogItem catalogItem, IRollingStock rollingStock)
        {
            return Task.CompletedTask;
        }

        public Task UpdateRollingStockAsync(ICatalogItem catalogItem, IRollingStock rollingStock)
        {
            return Task.CompletedTask;
        }

        public Task DeleteRollingStockAsync(ICatalogItem catalogItem, RollingStockId rollingStockId)
        {
            return Task.CompletedTask;
        }
    }
}
