using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.InMemory.Repositories.Collection
{
    public sealed class ShopsRepository : IShopsRepository
    {
        private readonly InMemoryContext _context;

        public ShopsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<ShopId> AddAsync(IShop shop)
        {
            throw new System.NotImplementedException();
        }

        public Task AddToFavouritesAsync(string user, ShopId shopId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IShopInfo>> GetFavouritesAsync(string user)
        {
            throw new System.NotImplementedException();
        }

        public Task<IShop> GetShopBySlugAsync(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<IShopInfo>> GetShopsAsync(Page page)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromFavouritesAsync(string user, ShopId shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}
