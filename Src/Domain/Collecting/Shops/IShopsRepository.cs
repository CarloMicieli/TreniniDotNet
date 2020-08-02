using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public interface IShopsRepository : IRepository<ShopId, Shop>
    {
        Task<bool> ExistsAsync(ShopId shopId);

        Task<bool> ExistsAsync(Slug slug);

        Task<Shop?> GetBySlugAsync(Slug slug);

        Task AddShopToFavouritesAsync(Owner owner, ShopId shopId);

        Task RemoveFromFavouritesAsync(Owner owner, ShopId shopId);

        Task<List<Shop>> GetFavouriteShopsAsync(Owner owner);
        
        Task<PaginatedResult<Shop>> GetAllAsync(Page page);
    }
}
