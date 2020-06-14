using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemsRepository;
        private readonly ICatalogItemsFactory _catalogItemsFactory;
        private readonly IBrandsRepository _brandsRepository;
        private readonly IScalesRepository _scales;
        private readonly IRailwaysRepository _railways;

        public CatalogItemService(
            ICatalogItemRepository catalogItemsRepository,
            ICatalogItemsFactory catalogItemsFactory,
            IBrandsRepository brands,
            IScalesRepository scales,
            IRailwaysRepository railways)
        {
            _catalogItemsRepository = catalogItemsRepository ??
                throw new ArgumentNullException(nameof(catalogItemsRepository));
            _catalogItemsFactory = catalogItemsFactory ??
                throw new ArgumentNullException(nameof(catalogItemsFactory));
            _brandsRepository = brands ??
                throw new ArgumentNullException(nameof(brands));
            _railways = railways ??
                throw new ArgumentNullException(nameof(railways));
            _scales = scales ??
                throw new ArgumentNullException(nameof(scales));
        }

        public Task<ICatalogItem?> GetBySlugAsync(Slug slug)
        {
            return _catalogItemsRepository.GetBySlugAsync(slug);
        }

        public Task<PaginatedResult<ICatalogItem>> GetLatestCatalogItemsAsync(Page page)
        {
            return _catalogItemsRepository.GetLatestCatalogItemsAsync(page);
        }

        public async Task<bool> ItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber)
        {
            var item = await _catalogItemsRepository.GetByBrandAndItemNumberAsync(brand, itemNumber);
            return item != null;
        }

        public Task<IBrandInfo?> FindBrandInfoBySlug(Slug brandSlug)
        {
            return _brandsRepository.GetInfoBySlugAsync(brandSlug);
        }

        public Task<IScaleInfo?> FindScaleInfoBySlug(Slug scale)
        {
            return _scales.GetInfoBySlugAsync(scale);
        }

        public Task<IRailwayInfo?> FindRailwayInfoBySlug(Slug railway)
        {
            return _railways.GetInfoBySlugAsync(railway);
        }

        public async Task<(Dictionary<Slug, IRailwayInfo> found, List<Slug> notFound)> FindRailwaysInfoBySlug(IEnumerable<Slug> railways)
        {
            var railwaysNotFound = new List<Slug>();
            var railwaysFound = new Dictionary<Slug, IRailwayInfo>();

            foreach (var railwaySlug in railways)
            {
                var railwayInfo = await FindRailwayInfoBySlug(railwaySlug);
                if (railwayInfo is null)
                {
                    railwaysNotFound.Add(railwaySlug);
                }
                else
                {
                    railwaysFound.Add(railwaySlug, railwayInfo);
                }
            }

            return (railwaysFound, railwaysNotFound);
        }


        public async Task<(CatalogItemId, Slug)> CreateNewCatalogItem(
            IBrandInfo brand,
            ItemNumber itemNumber,
            IScaleInfo scale,
            PowerMethod powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available)
        {
            var catalogItem = _catalogItemsFactory.CreateNewCatalogItem(
                brand,
                itemNumber,
                scale,
                powerMethod,
                rollingStocks,
                description,
                prototypeDescription,
                modelDescription,
                deliveryDate,
                available);
            var _ = await _catalogItemsRepository.AddAsync(catalogItem);
            return (catalogItem.Id, catalogItem.Slug);
        }

        public Task UpdateCatalogItem(ICatalogItem item,
            IBrandInfo? brand,
            ItemNumber? itemNumber,
            IScaleInfo? scale,
            PowerMethod? powerMethod,
            IReadOnlyList<IRollingStock> rollingStocks,
            string? description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool? available)
        {
            var modified = _catalogItemsFactory.UpdateCatalogItem(
                item,
                brand,
                itemNumber,
                scale,
                powerMethod,
                rollingStocks,
                description,
                prototypeDescription,
                modelDescription,
                deliveryDate,
                available);

            return _catalogItemsRepository.UpdateAsync(modified);
        }

        public Task AddRollingStockAsync(ICatalogItem catalogItem, IRollingStock rollingStock)
        {
            return _catalogItemsRepository.AddRollingStockAsync(catalogItem, rollingStock);
        }

        public Task UpdateRollingStockAsync(ICatalogItem catalogItem, IRollingStock rollingStock)
        {
            return _catalogItemsRepository.UpdateRollingStockAsync(catalogItem, rollingStock);
        }

        public Task DeleteRollingStockAsync(ICatalogItem catalogItem, RollingStockId rollingStockId)
        {
            return _catalogItemsRepository.DeleteRollingStockAsync(catalogItem, rollingStockId);
        }
    }
}
