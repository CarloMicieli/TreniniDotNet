using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Ef = TreniniDotNet.Infrastructure.Persistence.Catalog;
using D = TreniniDotNet.Domain.Catalog.CatalogItems;
using System.Linq;
using Infrastructure.Persistence.Catalog.CatalogItems;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<CatalogItemId> Add(D.CatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem.ToPersistence(_context));
            return Task.FromResult(catalogItem.CatalogItemId);
        }

        public async Task<ICatalogItem?> GetBy(IBrand brand, ItemNumber itemNumber)
        {
            var item = await _context.CatalogItems
                .Include(ci => ci.Brand)
                .Include(ci => ci.Scale)
                .Where(ci => ci.Brand.Slug == brand.Slug.ToString() && ci.ItemNumber == itemNumber.ToString())
                .SingleOrDefaultAsync();
            if (item is null)
            {
                return null;
            }

            return item.ToDomain();
        }

        public Task<ICatalogItem?> GetBy(Slug slug)
        {
            throw new NotImplementedException();
        }

        public Task<ICatalogItem?> GetBy(CatalogItemId id)
        {
            throw new NotImplementedException();
        }
    }
}
