using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BrandId> Add(IBrand brand)
        {
            if (brand is null)
            {
                throw new ArgumentNullException(nameof(brand));
            }

            Brand newBrand = (Brand)brand;
            _context.Brands.Add(newBrand);
            
            await _context.SaveChangesAsync();

            return newBrand.BrandId;
        }

        public async Task<IBrand> GetBy(BrandId brandId)
        {
            var brand = await _context.Brands
                .Where(c => c.BrandId == brandId)
                .SingleOrDefaultAsync();

            if (brand is null)
            {
                throw new BrandNotFoundException();
            }

            return brand;
        }

        public async Task<IBrand> GetBy(Slug slug)
        {
            var brand = await _context.Brands
                .Where(c => c.Slug == slug)
                .SingleOrDefaultAsync();

            if (brand is null)
            {
                throw new BrandNotFoundException();
            }

            return brand;
        }

        public async Task Update(IBrand brand)
        {
            await _context.Brands.AddAsync((Brand)brand);
        }
    }
}
