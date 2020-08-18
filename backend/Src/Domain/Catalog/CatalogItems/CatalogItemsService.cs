using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.DeliveryDates;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class CatalogItemsService : IDomainService
    {
        private readonly ICatalogItemsRepository _catalogItemsRepository;
        private readonly CatalogItemsFactory _catalogItemsFactory;
        private readonly IBrandsRepository _brandsRepository;
        private readonly IScalesRepository _scales;
        private readonly IRailwaysRepository _railways;

        public CatalogItemsService(
            CatalogItemsFactory catalogItemsFactory,
            ICatalogItemsRepository catalogItemsRepository,
            IBrandsRepository brands,
            IRailwaysRepository railways,
            IScalesRepository scales)
        {
            _catalogItemsRepository = catalogItemsRepository ?? throw new ArgumentNullException(nameof(catalogItemsRepository));
            _catalogItemsFactory = catalogItemsFactory ?? throw new ArgumentNullException(nameof(catalogItemsFactory));
            _brandsRepository = brands ?? throw new ArgumentNullException(nameof(brands));
            _railways = railways ?? throw new ArgumentNullException(nameof(railways));
            _scales = scales ?? throw new ArgumentNullException(nameof(scales));
        }

        public Task<CatalogItem?> GetBySlugAsync(Slug slug) => _catalogItemsRepository.GetBySlugAsync(slug);

        public Task<Railway?> FindRailwayBySlug(Slug slug) => _railways.GetBySlugAsync(slug);

        public Task<Brand?> FindBrandBySlug(Slug slug) => _brandsRepository.GetBySlugAsync(slug);

        public Task<bool> ItemAlreadyExists(Brand brand, ItemNumber itemNumber) =>
            _catalogItemsRepository.ExistsAsync(brand, itemNumber);

        public Task<Scale?> FindScaleBySlug(Slug slug) => _scales.GetBySlugAsync(slug);

        public async Task<(Dictionary<Slug, Railway> found, List<Slug> notFound)> FindRailwaysBySlug(
            IEnumerable<Slug> railways)
        {
            var railwaysNotFound = new List<Slug>();
            var railwaysFound = new Dictionary<Slug, Railway>();

            foreach (var railwaySlug in railways)
            {
                var railwayInfo = await FindRailwayBySlug(railwaySlug);
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

        public async Task<(CatalogItemId, Slug)> CreateCatalogItem(
            BrandRef brand,
            ItemNumber itemNumber,
            ScaleRef scale,
            PowerMethod powerMethod,
            string description,
            string? prototypeDescription,
            string? modelDescription,
            DeliveryDate? deliveryDate,
            bool available,
            IEnumerable<RollingStock> rollingStocks)
        {
            var catalogItem = _catalogItemsFactory.CreateCatalogItem(
                brand,
                itemNumber,
                scale,
                powerMethod,
                description,
                prototypeDescription,
                modelDescription,
                deliveryDate,
                available,
                rollingStocks);
            var _ = await _catalogItemsRepository.AddAsync(catalogItem);
            return (catalogItem.Id, catalogItem.Slug);
        }

        public Task UpdateCatalogItemAsync(CatalogItem catalogItem) =>
            _catalogItemsRepository.UpdateAsync(catalogItem);

        public Task<PaginatedResult<CatalogItem>> GetLatestCatalogItemsAsync(Page page) =>
            _catalogItemsRepository.GetLatestCatalogItemsAsync(page);
    }
}
