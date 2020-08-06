using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.IntegrationTests.Helpers.Data.MockRepositories
{
    public class CatalogItemsRepository : ICatalogItemsRepository
    {
        private InMemoryContext Context { get; }

        public CatalogItemsRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<PaginatedResult<CatalogItem>> GetAllAsync(Page page)
        {
            throw new System.NotImplementedException();
        }

        public Task<CatalogItemId> AddAsync(CatalogItem catalogItem)
        {
            Context.CatalogItems.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(CatalogItem brand)
        {
            Context.CatalogItems.RemoveAll(it => it.Id == brand.Id);
            Context.CatalogItems.Add(brand);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CatalogItemId id)
        {
            Context.CatalogItems.RemoveAll(it => it.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CatalogItem catalogItem)
        {
            Context.CatalogItems.RemoveAll(it => it.Id == catalogItem.Id);
            return Task.CompletedTask;
        }

        public Task<CatalogItem> GetBySlugAsync(Slug slug)
        {
            var catalogItem = Context.CatalogItems
                .FirstOrDefault(it => it.Slug == slug);
            return Task.FromResult(catalogItem);
        }

        public Task<PaginatedResult<CatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            var results = Context.CatalogItems.OrderByDescending(it => it.CreatedDate)
                .Take(page.Limit)
                .Skip(page.Start);
            return Task.FromResult(new PaginatedResult<CatalogItem>(page, results));
        }

        public Task<bool> ExistsAsync(Brand brand, ItemNumber itemNumber)
        {
            var exists = Context.CatalogItems
                .Any(it => it?.Brand.Id == brand.Id && it.ItemNumber == itemNumber);
            return Task.FromResult(exists);
        }
    }
}
