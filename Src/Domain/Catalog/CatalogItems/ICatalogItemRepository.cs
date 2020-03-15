using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemRepository
    {
        Task<bool> Exists(IBrand brand, ItemNumber itemNumber);

        Task<ICatalogItem?> GetBy(IBrand brand, ItemNumber itemNumber);

        Task<ICatalogItem?> GetBy(Slug slug);

        Task<CatalogItemId> Add(CatalogItem catalogItem);
    }
}