using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collection;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public class ApplicationContextSeed
    {
        public static void SeedCatalog(IServiceProvider scopedServices)
        {
            IBrandsRepository brands = scopedServices.GetRequiredService<IBrandsRepository>();
            brands.SeedDatabase();

            IRailwaysRepository railways = scopedServices.GetRequiredService<IRailwaysRepository>();
            railways.SeedDatabase();

            IScalesRepository scales = scopedServices.GetRequiredService<IScalesRepository>();
            scales.SeedDatabase();

            ICatalogItemRepository catalogItems = scopedServices.GetRequiredService<ICatalogItemRepository>();
            catalogItems.SeedDatabase();
        }

        public static async Task SeedCollections(IServiceProvider scopedServices)
        {
            var shops = scopedServices.GetRequiredService<IShopsRepository>();
            await shops.SeedDatabase();

            var collections = scopedServices.GetRequiredService<ICollectionsRepository>();
            var collectionItems = scopedServices.GetRequiredService<ICollectionItemsRepository>();
            await collections.SeedDatabase(collectionItems);

            var wishLists = scopedServices.GetRequiredService<IWishlistsRepository>();
            var wishListItems = scopedServices.GetRequiredService<IWishlistItemsRepository>();
            await wishLists.SeedDatabase(wishListItems);
        }
    }
}