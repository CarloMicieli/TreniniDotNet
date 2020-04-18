using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShopsRepository
    {
        Task<IEnumerable<IShopInfo>> GetFavouritesAsync(string user);

        Task AddToFavouritesAsync(string user, ShopId shopId);

        Task RemoveFromFavouritesAsync(string user, ShopId shopId);

        Task<IShopInfo> GetShopInfoBySlugAsync(Slug slug);

        Task<bool> ExistsAsync(Slug slug);

        Task<ShopId> AddAsync(IShop shop);

        Task<IShop> GetBySlugAsync(Slug slug);

        Task<IEnumerable<IShop>> GetShopsAsync(Page page);
    }
}
