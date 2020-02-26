using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBrandsFactory _brandsFactory;

        public BrandsRepository(ApplicationDbContext context, IBrandsFactory brandsFactory)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _brandsFactory = brandsFactory ??
                throw new ArgumentNullException(nameof(brandsFactory));
        }

        public Task<BrandId> Add(BrandId brandId, string name, Slug slug, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind? brandKind)
        {
            var newBrand = new Brand
            {
                BrandId = brandId.ToGuid(),
                Name = name,
                Slug = slug.ToString(),
                CompanyName = companyName,
                WebsiteUrl = websiteUrl?.ToString(),
                EmailAddress = emailAddress?.ToString(),
                BrandKind = brandKind?.ToString()
            };

            _context.Brands.Add(newBrand);
            return Task.FromResult(brandId);
        }

        public Task<bool> Exists(Slug slug)
        {
            return _context.Brands.AnyAsync(b => b.Slug == slug.ToString());
        }

        public Task<List<IBrand>> GetAll()
        {
            return _context.Brands
                .Select(b => _brandsFactory.NewBrand(
                    b.BrandId,
                    b.Name,
                    b.Slug,
                    b.CompanyName,
                    b.WebsiteUrl,
                    b.EmailAddress,
                    b.BrandKind))
                .ToListAsync();
        }

        public async Task<PaginatedResult<IBrand>> GetBrands(Page page)
        {
            var results = await _context.Brands
                .Skip(page.Start)
                .Take(page.Limit + 1) //To discover if we have more results
                .OrderBy(b => b.Name)
                .Select(b => _brandsFactory.NewBrand(
                    b.BrandId,
                    b.Name,
                    b.Slug,
                    b.CompanyName,
                    b.WebsiteUrl,
                    b.EmailAddress,
                    b.BrandKind))
                .ToListAsync();

            return new PaginatedResult<IBrand>(
                page,
                results);
        }

        public Task<IBrand> GetBy(Slug slug)
        {
            return _context.Brands
                .Where(b => b.Slug == slug.ToString())
                .Select(b => _brandsFactory.NewBrand(
                    b.BrandId,
                    b.Name,
                    b.Slug,
                    b.CompanyName,
                    b.WebsiteUrl,
                    b.EmailAddress,
                    b.BrandKind))
                .FirstOrDefaultAsync();
        }
    }
}
