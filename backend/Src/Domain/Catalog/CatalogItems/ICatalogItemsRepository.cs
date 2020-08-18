using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemsRepository : IRepository<CatalogItemId, CatalogItem>
    {
        Task<CatalogItem?> GetBySlugAsync(Slug slug);

        Task<PaginatedResult<CatalogItem>> GetLatestCatalogItemsAsync(Page page);

        Task<bool> ExistsAsync(Brand brand, ItemNumber itemNumber);
    }
}
