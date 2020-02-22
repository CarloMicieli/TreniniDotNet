using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.InMemory.Catalog
{
    public class BrandRepository : IBrandsRepository
    {
        private readonly InMemoryContext _context;

        public BrandRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<BrandId> Add(IBrand brand)
        {
            _context.Brands.Add((Brand)brand);
            return Task.FromResult(BrandId.Empty); //TODO: fixme
        }

        public Task<IBrand> GetBy(BrandId brandId)
        {
            var brand = _context.Brands.FirstOrDefault(e => e.BrandId == brandId);
            return Task.FromResult(brand);
        }

        public Task<IBrand> GetBy(Slug slug)
        {
            IBrand brand = _context.Brands.FirstOrDefault(e => e.Slug == slug);
            if (brand == null)
            {
                throw new BrandNotFoundException();
            }
            return Task.FromResult(brand);
        }

        public async Task Update(IBrand brand)
        {
            var brandToUpdate = _context.Brands.FirstOrDefault(e => e.BrandId.Equals(brand.BrandId)); //TODO: #fixme
            if (brandToUpdate != null)
            {
                //bran
            }

            await Task.CompletedTask;
        }
    }
}
