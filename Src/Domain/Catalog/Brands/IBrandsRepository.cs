using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsRepository
    {
        Task<IBrand?> GetBySlug(Slug slug);

        Task<IBrand?> GetByName(string name);

        Task<BrandId> Add(IBrand brand);

        Task<bool> Exists(Slug slug);

        Task<List<IBrand>> GetAll();

        Task<PaginatedResult<IBrand>> GetBrands(Page page);
    }
}
