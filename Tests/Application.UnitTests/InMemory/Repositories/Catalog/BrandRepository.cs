using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.InMemory.Repositories.Catalog
{
    public class BrandRepository : IBrandsRepository
    {
        private readonly InMemoryContext _context;
        private readonly IBrandsFactory _brandsFactory;

        public BrandRepository(InMemoryContext context)
        {
            _context = context;
            _brandsFactory = new BrandsFactory();
        }

        public Task<BrandId> Add(BrandId brandId, string name, Slug slug, string companyName, Uri websiteUrl, MailAddress emailAddress, BrandKind? brandKind)
        {
            var newBrand = _brandsFactory.NewBrand(
                brandId.ToGuid(),
                name,
                slug.ToString(),
                companyName,
                websiteUrl?.ToString(),
                emailAddress?.ToString(),
                brandKind?.ToString());

            _context.Brands.Add(newBrand);

            return Task.FromResult(brandId);
        }

        public Task<BrandId> Add(IBrand brand)
        {
            _context.Brands.Add(brand);
            return Task.FromResult(brand.BrandId);
        }

        public Task<bool> Exists(Slug slug)
        {
            bool exists = _context.Brands.Any(b => b.Slug == slug);
            return Task.FromResult(exists);
        }

        public Task<List<IBrand>> GetAll()
        {
            return Task.FromResult(_context.Brands.ToList());
        }

        public Task<PaginatedResult<IBrand>> GetBrands(Page page)
        {
            var results = _context.Brands
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<IBrand>(page, results));
        }

        public Task<IBrand> GetByName(string name)
        {
            IBrand brand = _context.Brands
                .FirstOrDefault(e => e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(brand);
        }

        public Task<IBrand> GetBySlug(Slug slug)
        {
            IBrand brand = _context.Brands.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(brand);
        }
    }
}
