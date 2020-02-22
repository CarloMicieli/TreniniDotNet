using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Net.Mail;

namespace TreniniDotNet.Application.InMemory.Catalog
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

        public Task<bool> Exists(Slug slug)
        {
            bool exists = _context.Brands.Any(b => b.Slug == slug);
            return Task.FromResult(exists);
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
    }
}
