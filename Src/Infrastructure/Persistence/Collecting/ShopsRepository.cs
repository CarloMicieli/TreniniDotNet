using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting
{
    public sealed class ShopsRepository : EfCoreRepository<ShopId, Shop>, IShopsRepository
    {
        public ShopsRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistsAsync(ShopId shopId) =>
            DbContext.Shops.AnyAsync(it => it.Id == shopId);

        public Task<bool> ExistsAsync(Slug slug) =>
            DbContext.Shops.AnyAsync(it => it.Slug == slug);

        public Task<Shop?> GetBySlugAsync(Slug slug) =>
#pragma warning disable 8619
            DbContext.Shops.FirstOrDefaultAsync(it => it.Slug == slug);
#pragma warning restore 8619
    }
}
