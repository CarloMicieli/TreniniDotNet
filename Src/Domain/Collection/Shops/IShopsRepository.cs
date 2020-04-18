using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public interface IShopsRepository
    {
        Task<IEnumerable<IShopInfo>> GetShopsAsync(Page page);

        Task<IEnumerable<IShopInfo>> GetFavouritesAsync(string user);

        Task AddToFavouritesAsync(string user, ShopId shopId);

        Task RemoveFromFavouritesAsync(string user, ShopId shopId);

        Task<IShop> GetShopBySlugAsync(Slug slug);

        Task<IShopInfo> GetShopInfoBySlugAsync(Slug slug);

        // USATI
        Task<bool> ExistsAsync(Slug slug);

        Task<ShopId> AddAsync(IShop shop);
    }
}
