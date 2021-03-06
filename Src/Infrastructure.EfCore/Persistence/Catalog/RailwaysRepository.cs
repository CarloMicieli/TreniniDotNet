using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog
{
    public sealed class RailwaysRepository : EfCoreRepository<RailwayId, Railway>, IRailwaysRepository
    {
        public RailwaysRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistsAsync(Slug slug) =>
            DbContext.Railways.AnyAsync(it => it.Slug == slug);

        public override Task<PaginatedResult<Railway>> GetAllAsync(Page page) => 
            DbContext.Railways.ToPaginatedResultAsync(page, b => b.Name);
        
        public Task<Railway?> GetBySlugAsync(Slug slug) =>
#pragma warning disable 8619
            DbContext.Railways
               .FirstOrDefaultAsync(it => it.Slug == slug);
#pragma warning restore 8619
    }
}
