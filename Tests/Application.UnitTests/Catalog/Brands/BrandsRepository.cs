using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private InMemoryContext Context { get; }

        public BrandsRepository(InMemoryContext context)
        {
            Context = context;
        }

        public Task<bool> ExistsAsync(BrandId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Brand> GetByIdAsync(BrandId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PaginatedResult<Brand>> GetAllAsync(Page page)
        {
            var results = Context.Brands
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<Brand>(page, results));
        }

        public Task<BrandId> AddAsync(Brand catalogItem)
        {
            Context.Brands.Add(catalogItem);
            return Task.FromResult(catalogItem.Id);
        }

        public Task UpdateAsync(Brand brand)
        {
            var indexOf = Context.Brands
                .RemoveAll(it => it.Id == brand.Id);
            Context.Brands.Add(brand);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(BrandId id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Brand brand)
        {
            throw new System.NotImplementedException();
        }


        public Task<bool> ExistsAsync(Slug slug)
        {
            bool exists = Context.Brands.Any(b => b.Slug == slug);
            return Task.FromResult(exists);
        }

        public Task<Brand> GetBySlugAsync(Slug slug)
        {
            var brand = Context.Brands.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(brand);
        }
    }
}
