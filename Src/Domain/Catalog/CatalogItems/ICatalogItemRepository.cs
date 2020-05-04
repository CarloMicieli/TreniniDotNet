using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemRepository
    {
        Task<bool> ExistsAsync(IBrandInfo brand, ItemNumber itemNumber);

        Task<ICatalogItem?> GetByBrandAndItemNumberAsync(IBrandInfo brand, ItemNumber itemNumber);

        Task<ICatalogItem?> GetBySlugAsync(Slug slug);

        Task<CatalogItemId> AddAsync(ICatalogItem catalogItem);

        Task UpdateAsync(ICatalogItem catalogItem);

        Task<PaginatedResult<ICatalogItem>> GetLatestCatalogItemsAsync(Page page);

        Task AddRollingStockAsync(ICatalogItem catalogItem, IRollingStock rollingStock);

        Task UpdateRollingStockAsync(ICatalogItem catalogItem, IRollingStock rollingStock);

        Task DeleteRollingStockAsync(ICatalogItem catalogItem, RollingStockId rollingStockId);
    }
}
