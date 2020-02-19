using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public interface ICatalogItemRepository
    {
         Task<CatalogItem> GetBy(IBrand brand, ItemNumber itemNumber);

         Task<CatalogItem> GetBy(Slug slug);

         Task<CatalogItem> GetBy(CatalogItemId id);

         Task<CatalogItemId> Add(CatalogItem catalogItem);
    }
}