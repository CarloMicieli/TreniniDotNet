using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog
{
    public sealed class ScalesRepository : EfCoreRepository<ScaleId, Scale>, IScalesRepository
    {
        public ScalesRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistsAsync(Slug slug) =>
            DbContext.Scales.AnyAsync(it => it.Slug == slug);

        public override Task<PaginatedResult<Scale>> GetAllAsync(Page page) =>
            DbContext.Scales.ToPaginatedResultAsync(page, x => x.Name);

        public Task<Scale?> GetBySlugAsync(Slug slug) =>
#pragma warning disable 8619
            DbContext.Scales
                .FirstOrDefaultAsync(it => it.Slug == slug);
#pragma warning restore 8619
    }
}
