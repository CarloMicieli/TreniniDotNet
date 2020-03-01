using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

using Ef = TreniniDotNet.Infrastructure.Persistence.Catalog;
using D = TreniniDotNet.Domain.Catalog.CatalogItems;

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
            _context.CatalogItems.Add(catalogItem.ToPersistence());
            return Task.FromResult(catalogItem.CatalogItemId);
        }

        Task<ICatalogItem?> ICatalogItemRepository.GetBy(IBrand brand, ItemNumber itemNumber)
        {
            throw new NotImplementedException();
        }

        Task<ICatalogItem?> ICatalogItemRepository.GetBy(Slug slug)
        {
            throw new NotImplementedException();
        }

        Task<ICatalogItem?> ICatalogItemRepository.GetBy(CatalogItemId id)
        {
            throw new NotImplementedException();
        }
    }
}
