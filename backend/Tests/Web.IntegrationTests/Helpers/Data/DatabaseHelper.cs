using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public static class DatabaseHelper
    {
        public static async Task InitialiseDbForTests(IServiceProvider serviceProvider)
        {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            await SeedCatalog(serviceProvider, unitOfWork);
            await SeedCollections(serviceProvider, unitOfWork);

            await unitOfWork.SaveAsync();
        }

        private static async Task SeedCatalog(IServiceProvider scopedServices, IUnitOfWork unitOfWork)
        {
            var brands = scopedServices.GetRequiredService<IBrandsRepository>();
            await brands.SeedDatabase();

            var railways = scopedServices.GetRequiredService<IRailwaysRepository>();
            await railways.SeedDatabase();

            var scales = scopedServices.GetRequiredService<IScalesRepository>();
            await scales.SeedDatabase();

            var catalogItems = scopedServices.GetRequiredService<ICatalogItemsRepository>();
            await catalogItems.SeedDatabase();
        }

        private static async Task SeedCollections(IServiceProvider scopedServices, IUnitOfWork unitOfWork)
        {
            var shops = scopedServices.GetRequiredService<IShopsRepository>();
            await shops.SeedDatabase();

            var collections = scopedServices.GetRequiredService<ICollectionsRepository>();
            await collections.SeedDatabase();

            var wishLists = scopedServices.GetRequiredService<IWishlistsRepository>();
            await wishLists.SeedDatabase();
        }
    }
}