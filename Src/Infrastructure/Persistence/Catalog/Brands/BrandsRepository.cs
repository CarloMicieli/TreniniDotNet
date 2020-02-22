using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public sealed class BrandsRepository : IBrandsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BrandsRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            return new BrandId(newBrand.BrandId);
        }

        public Task<IBrand> GetBy(BrandId brandId)
        {
            //var brand = await _context.Brands
            //    .Where(c => c.BrandId == brandId.ToGuid())
            //    .SingleOrDefaultAsync();

            //if (brand is null)
            //{
            //    throw new BrandNotFoundException();
            //}

            //return brand;
            throw new NotImplementedException();
        }

        public async Task<IBrand> GetBy(Slug slug)
        {
            var brand = await _context.Brands
                .Where(c => c.Slug == slug.ToString())
                .ProjectTo<Domain.Catalog.Brands.Brand>(_mapper.ConfigurationProvider)
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
