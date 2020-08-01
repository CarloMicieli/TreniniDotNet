using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog
{
    public sealed class CatalogItemsRepository : EfCoreRepository<CatalogItemId, CatalogItem>, ICatalogItemsRepository
    {
        public CatalogItemsRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<CatalogItem?> GetBySlugAsync(Slug slug) =>
#pragma warning disable 8619
            EagerLoadedCatalogItems()
                .FirstOrDefaultAsync(it => it.Slug == slug);
#pragma warning restore 8619

        public async Task<PaginatedResult<CatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            var results = await EagerLoadedCatalogItems()
                .OrderByDescending(it => it.CreatedDate)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToListAsync();
            return new PaginatedResult<CatalogItem>(page, results);
        }

        public Task<bool> ExistsAsync(Brand brand, ItemNumber itemNumber) =>
            DbContext.CatalogItems.AnyAsync(it => it.Brand.Id == brand.Id && it.ItemNumber == itemNumber);

        public override Task<CatalogItemId> AddAsync(CatalogItem catalogItem)
        {
            DbContext.Entry(catalogItem.Brand).State = EntityState.Unchanged;
            DbContext.Entry(catalogItem.Scale).State = EntityState.Unchanged;
            
            foreach (var rs in catalogItem.RollingStocks)
            {
                DbContext.Entry(rs.Railway).State = EntityState.Unchanged;
            }
            
            DbContext.CatalogItems.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }
        
        public override Task UpdateAsync(CatalogItem catalogItem)
        {
            DbContext.Entry(catalogItem.Brand).State = EntityState.Unchanged;
            DbContext.Entry(catalogItem.Scale).State = EntityState.Unchanged;
            
            foreach (var rs in catalogItem.RollingStocks)
            {
                var r = DbContext.Railways.Find(rs.Railway.Id);
                DbContext.Entry(rs.Railway).State = EntityState.Unchanged;
            }
            
            DbContext.CatalogItems.Update(catalogItem);
            return Task.CompletedTask;
        }

        private IQueryable<CatalogItem> EagerLoadedCatalogItems() =>
            DbContext.CatalogItems
                .Include(c => c.Brand)
                .Include(c => c.Scale)
                .Include(c => c.RollingStocks)
                .ThenInclude(rs => rs.Railway);
    }
}
