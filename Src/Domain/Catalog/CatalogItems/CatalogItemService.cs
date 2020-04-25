using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemsRepository;
        private readonly IBrandsRepository _brandsRepository;
        private readonly IScalesRepository _scales;
        private readonly IRailwaysRepository _railways;

        public CatalogItemService(
            ICatalogItemRepository catalogItemsRepository,
            IBrandsRepository brands,
            IScalesRepository scales,
            IRailwaysRepository railways)
        {
            _catalogItemsRepository = catalogItemsRepository ??
                throw new ArgumentNullException(nameof(catalogItemsRepository));
            _brandsRepository = brands ??
                throw new ArgumentNullException(nameof(brands));
            _railways = railways ??
                throw new ArgumentNullException(nameof(railways));
            _scales = scales ??
                throw new ArgumentNullException(nameof(scales));
        }

        public Task<ICatalogItem?> FindBySlug(Slug slug)
        {
            return _catalogItemsRepository.GetBySlugAsync(slug);
        }

        public async Task<bool> ItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber)
        {
            var item = await _catalogItemsRepository.GetByAsync(brand, itemNumber);
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

        public Task<CatalogItemId> CreateNewCatalogItem(ICatalogItem catalogItem)
        {
            return _catalogItemsRepository.AddAsync(catalogItem);
        }
    }
}