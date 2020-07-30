using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting
{
    public sealed class CollectionsRepository : EfCoreRepository<CollectionId, Collection>, ICollectionsRepository
    {
        public CollectionsRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistsAsync(CollectionId id) =>
            DbContext.Collections.AnyAsync(it => it.Id == id);

        public Task<Collection?> GetByIdAsync(CollectionId id) =>
#pragma warning disable 8619
            CollectionQuery()
                .FirstOrDefaultAsync(it => it.Id == id);
#pragma warning restore 8619

        public Task<Collection?> GetByOwnerAsync(Owner owner) =>
#pragma warning disable 8619
            CollectionQuery()
                .FirstOrDefaultAsync(it => it.Owner == owner);
#pragma warning restore 8619

        public Task<bool> ExistsAsync(Owner owner) =>
            DbContext.Collections.AnyAsync(it => it.Owner == owner);

        private IQueryable<Collection> CollectionQuery() =>
            DbContext.Collections
                .Include(x => x.Items)
                .ThenInclude(it => it.PurchasedAt)
                .Include(x => x.Items)
                .ThenInclude(it => it.CatalogItem)
                .ThenInclude(it => it.RollingStocks)
                .ThenInclude(rs => rs.Railway)
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(ci => ci.Brand)
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(ci => ci.Scale);
    }
}
