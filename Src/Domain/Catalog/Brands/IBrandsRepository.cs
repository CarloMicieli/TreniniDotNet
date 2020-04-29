using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsRepository
    {
        Task<BrandId> AddAsync(IBrand brand);

        Task<bool> ExistsAsync(Slug slug);

        Task<PaginatedResult<IBrand>> GetBrandsAsync(Page page);

        Task<IBrand?> GetBySlugAsync(Slug slug);

        Task<IBrandInfo?> GetInfoBySlugAsync(Slug slug);

        Task UpdateAsync(IBrand brand);
    }
}
