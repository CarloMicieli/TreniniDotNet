using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.InMemory.Collecting.Shops
{
    public sealed class ShopsRepository : IShopsRepository
    {
        private readonly InMemoryContext _context;

        public ShopsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task AddToFavouritesAsync(string user, ShopId shopId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IShopInfo> GetShopInfoBySlugAsync(Slug slug)
        {
            IShopInfo result = _context.Shops.FirstOrDefault(it => it.Slug == slug);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<IShopInfo>> GetFavouritesAsync(string user)
        {
            throw new System.NotImplementedException();
        }

        public Task<IShop> GetBySlugAsync(Slug slug)
        {
            var result = _context.Shops.FirstOrDefault(it => it.Slug == slug);
            return Task.FromResult(result);
        }

        public Task RemoveFromFavouritesAsync(string user, ShopId shopId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            var result = _context.Shops.Any(it => it.Slug == slug);
            return Task.FromResult(result);
        }

        public Task<ShopId> AddAsync(IShop shop) =>
            Task.FromResult(shop.ShopId);

        public Task<IEnumerable<IShop>> GetShopsAsync(Page page)
        {
            var result = _context.Shops.Skip(page.Start).Take(page.Limit);
            return Task.FromResult(result);
        }
    }
}
