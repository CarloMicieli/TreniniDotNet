using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Collecting.Shared;
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

        public Task AddShopToFavouritesAsync(Owner owner, ShopId shopId)
        {
            DbContext.ShopFavourites.Add(new ShopFavourite
            {
                ShopId = shopId,
                Owner = owner
            });
            return Task.CompletedTask;
        }

        public async Task RemoveFromFavouritesAsync(Owner owner, ShopId shopId)
        {
            var element = await DbContext.ShopFavourites
                .FirstOrDefaultAsync(it => it.Owner == owner && it.ShopId == shopId);

            if (element != null)
            {
                DbContext.ShopFavourites.Remove(element);
            }
        }

        public Task<List<Shop>> GetFavouriteShopsAsync(Owner owner) =>
            DbContext.ShopFavourites
                .Include(x => x.Shop)
                .Where(it => it.Owner == owner)
                .Select(it => it.Shop)
                .ToListAsync();
    }
}
