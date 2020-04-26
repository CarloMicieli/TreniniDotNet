using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemRepository
    {
        Task<bool> ExistsAsync(IBrandInfo brand, ItemNumber itemNumber);

        Task<ICatalogItem?> GetByBrandAndItemNumberAsync(IBrandInfo brand, ItemNumber itemNumber);

        Task<ICatalogItem?> GetBySlugAsync(Slug slug);

        Task<CatalogItemId> AddAsync(ICatalogItem catalogItem);

        Task UpdateAsync(ICatalogItem catalogItem);
    }
}