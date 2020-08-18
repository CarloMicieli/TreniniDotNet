using System.Threading.Tasks;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Shared
{
    public interface ICatalogItemRefsRepository
    {
        Task<CatalogItemRef?> GetCatalogItemAsync(Slug slug);
    }
}