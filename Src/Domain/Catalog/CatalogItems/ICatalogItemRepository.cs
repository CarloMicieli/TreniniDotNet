using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemRepository
    {
         Task<ICatalogItem> GetBy(IBrand brand, ItemNumber itemNumber);

         Task<ICatalogItem> GetBy(Slug slug);

         Task<ICatalogItem> GetBy(CatalogItemId id);

         Task<CatalogItemId> Add(ICatalogItem catalogItem);
    }
}