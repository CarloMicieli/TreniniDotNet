using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsRepository
    {
        Task<IBrand> GetBy(Slug slug);

        //Task<IBrand> GetBy(BrandId brandId);

        Task<BrandId> Add(IBrand brand);

        //Task Update(IBrand brand);
    }
}
