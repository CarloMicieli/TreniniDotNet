using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastructure.Persistence;

using Ef = TreniniDotNet.Infrastructure.Persistence.Catalog;

namespace TreniniDotNet.Infrastracture.Persistence.Catalog.CatalogItems
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CatalogItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<CatalogItemId> Add(CatalogItem catalogItem)
        {
            var brand = new Ef.Brands.Brand
            {
                BrandId = catalogItem.Brand.BrandId.ToGuid()
            };

            var scale = new Ef.Scales.Scale
            {
                ScaleId = catalogItem.Scale.ScaleId.ToGuid()
            };

            IEnumerable<RollingStock> rollingStocks = catalogItem.RollingStocks
                .Select(rs => new RollingStock {
                    RollingStockId = new Guid(),
                    Category = rs.Category.ToString(),
                    Era = rs.Era.ToString(),
                    Length = (decimal) rs.Length.ToMillimeters(),
                    Railway = new Ef.Railways.Railway { RailwayId = rs.Railway.RailwayId.ToGuid() },
                    ClassName = rs.ClassName,
                    RoadNumber = rs.RoadNumber
                });

            _context.CatalogItems.Add(new Ef.CatalogItems.CatalogItem 
            {
                CatalogItemId = catalogItem.CatalogItemId.ToGuid(),
                Brand = brand,
                ItemNumber = catalogItem.ItemNumber.ToString(),
                PowerMethod = catalogItem.PowerMethod.ToString(),
                Slug = catalogItem.Slug.ToString(),
                Scale = scale,
                ModelDescription = catalogItem.ModelDescription,
                PrototypeDescription = catalogItem.PrototypeDescription,
                Description = catalogItem.Description,
                RollingStocks = rollingStocks.ToList()
            });
            return Task.FromResult(catalogItem.CatalogItemId);
        }

        public Task<CatalogItem> GetBy(IBrand brand, ItemNumber itemNumber)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogItem> GetBy(Slug slug)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogItem> GetBy(CatalogItemId id)
        {
            throw new NotImplementedException();
        }
    }
}
