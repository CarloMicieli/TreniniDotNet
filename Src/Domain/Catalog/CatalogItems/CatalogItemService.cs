using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemsRepository;
        private readonly ICatalogItemFactory _catalogItemFactory;

        private readonly IBrandsRepository _brands;
        private readonly IRailwaysRepository _railways;
        private readonly IScalesRepository _scales;

        public CatalogItemService(
            ICatalogItemRepository catalogItemsRepository, 
            ICatalogItemFactory catalogItemFactory, 
            IBrandsRepository brands, 
            IRailwaysRepository railways, 
            IScalesRepository scales)
        {
            _catalogItemsRepository = catalogItemsRepository;
            _catalogItemFactory = catalogItemFactory;
            _brands = brands;
            _railways = railways;
            _scales = scales;
        }

        public async Task CreateCatalogItem(
            BrandId brandId,
            ItemNumber itemNumber,
            string? description,
            string? modelDescription,
            string? prototypeDescription,
            IRollingStock rollingStock)
        {/*
            IBrand brand = await _brands.GetBy(brandId);
            IRailway railway = await _railways.GetBy(rollingStock.Railway.Slug);
            IScale scale = await _scales.GetBy(rollingStock.Scale.Slug);

            CatalogItem item = await _catalogItemsRepository.GetBy(brand, itemNumber);

            CatalogItem newItem = _catalogItemFactory.NewCatalogItem();

            await _catalogItemsRepository.Add(newItem);
            */



            throw new NotImplementedException();
        }
    }
}