using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public interface IShopsRepository
    {
        Task<IEnumerable<IShopInfo>> GetFavouritesAsync(string user);

        Task AddToFavouritesAsync(string user, ShopId shopId);

        Task RemoveFromFavouritesAsync(string user, ShopId shopId);

        Task<IShopInfo?> GetShopInfoBySlugAsync(Slug slug);

        Task<bool> ExistsAsync(Slug slug);

        Task<ShopId> AddAsync(IShop shop);

        Task<IShop?> GetBySlugAsync(Slug slug);

        Task<IEnumerable<IShop>> GetShopsAsync(Page page);
    }
}
