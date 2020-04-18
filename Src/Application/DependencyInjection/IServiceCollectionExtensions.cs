using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.CatalogItems;

using CatalogUseCases = TreniniDotNet.Application.UseCases.Catalog;
using CatalogBoundaries = TreniniDotNet.Application.Boundaries.Catalog;
using CollectionUseCases = TreniniDotNet.Application.UseCases.Collection;
using CollectionBoundaries = TreniniDotNet.Application.Boundaries.Collection;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddBrandUseCases();
            services.AddScaleUseCases();
            services.AddRailwayUseCases();
            services.AddCatalogItemUseCases();

            services.AddCollectionUseCases();
            services.AddShopUseCases();
            services.AddWishListUseCases();

            services.AddScoped(typeof(IUseCaseInputValidator<>), typeof(UseCaseInputValidator<>));

            return services;
        }

        private static IServiceCollection AddBrandUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateBrand.ICreateBrandUseCase, CatalogUseCases.CreateBrand>();
            services.AddScoped<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugUseCase, CatalogUseCases.GetBrandBySlug>();
            services.AddScoped<CatalogBoundaries.GetBrandsList.IGetBrandsListUseCase, CatalogUseCases.GetBrandsList>();
            services.AddScoped<IBrandsFactory, BrandsFactory>();

            services.AddScoped<BrandService>();

            return services;
        }

        private static IServiceCollection AddScaleUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateScale.ICreateScaleUseCase, CatalogUseCases.CreateScale>();
            services.AddScoped<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugUseCase, CatalogUseCases.GetScaleBySlug>();
            services.AddScoped<CatalogBoundaries.GetScalesList.IGetScalesListUseCase, CatalogUseCases.GetScalesList>();
            services.AddScoped<IScalesFactory, ScalesFactory>();

            services.AddScoped<ScaleService>();

            return services;
        }

        private static IServiceCollection AddRailwayUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateRailway.ICreateRailwayUseCase, CatalogUseCases.CreateRailway>();
            services.AddScoped<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugUseCase, CatalogUseCases.GetRailwayBySlug>();
            services.AddScoped<CatalogBoundaries.GetRailwaysList.IGetRailwaysListUseCase, CatalogUseCases.GetRailwaysList>();
            services.AddScoped<IRailwaysFactory, RailwaysFactory>();

            services.AddScoped<RailwayService>();

            return services;
        }

        private static IServiceCollection AddCatalogItemUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateCatalogItem.ICreateCatalogItemUseCase, CatalogUseCases.CreateCatalogItem>();
            services.AddScoped<CatalogBoundaries.GetCatalogItemBySlug.IGetCatalogItemBySlugUseCase, CatalogUseCases.GetCatalogItemBySlug>();
            services.AddScoped<ICatalogItemsFactory, CatalogItemsFactory>();

            services.AddScoped<CatalogItemService>();

            return services;
        }

        private static IServiceCollection AddCollectionUseCases(this IServiceCollection services)
        {
            services.AddScoped<CollectionBoundaries.AddItemToCollection.IAddItemToCollectionUseCase, CollectionUseCases.Collections.AddItemToCollection>();
            services.AddScoped<CollectionBoundaries.EditCollectionItem.IEditCollectionItemUseCase, CollectionUseCases.Collections.EditCollectionItem>();
            services.AddScoped<CollectionBoundaries.GetCollectionByOwner.IGetCollectionByOwnerUseCase, CollectionUseCases.Collections.GetCollectionByOwner>();
            services.AddScoped<CollectionBoundaries.CreateCollection.ICreateCollectionUseCase, CollectionUseCases.Collections.CreateCollection>();
            services.AddScoped<CollectionBoundaries.RemoveItemFromCollection.IRemoveItemFromCollectionUseCase, CollectionUseCases.Collections.RemoveItemFromCollection>();
            services.AddScoped<CollectionBoundaries.GetCollectionStatistics.IGetCollectionStatisticsUseCase, CollectionUseCases.Collections.GetCollectionStatistics>();

            services.AddScoped<ICollectionsFactory, CollectionsFactory>();

            return services;
        }

        private static IServiceCollection AddShopUseCases(this IServiceCollection services)
        {
            services.AddScoped<CollectionBoundaries.AddShopToFavourites.IAddShopToFavouritesUseCase, CollectionUseCases.Shops.AddShopToFavourites>();
            services.AddScoped<CollectionBoundaries.CreateShop.ICreateShopUseCase, CollectionUseCases.Shops.CreateShop>();
            services.AddScoped<CollectionBoundaries.RemoveShopFromFavourites.IRemoveShopFromFavouritesUseCase, CollectionUseCases.Shops.RemoveShopFromFavourites>();
            services.AddScoped<CollectionBoundaries.GetFavouriteShops.IGetFavouriteShopsUseCase, CollectionUseCases.Shops.GetFavouriteShops>();
            services.AddScoped<CollectionBoundaries.GetShopBySlug.IGetShopBySlugUseCase, CollectionUseCases.Shops.GetShopBySlug>();
            services.AddScoped<CollectionBoundaries.GetShopsList.IGetShopsListUseCase, CollectionUseCases.Shops.GetShopsList>();

            services.AddScoped<IShopsFactory, ShopsFactory>();

            return services;
        }

        private static IServiceCollection AddWishListUseCases(this IServiceCollection services)
        {
            services.AddScoped<CollectionBoundaries.AddItemToWishlist.IAddItemToWishlistUseCase, CollectionUseCases.Wishlists.AddItemToWishlist>();
            services.AddScoped<CollectionBoundaries.CreateWishlist.ICreateWishlistUseCase, CollectionUseCases.Wishlists.CreateWishlist>();
            services.AddScoped<CollectionBoundaries.DeleteWishlist.IDeleteWishlistUseCase, CollectionUseCases.Wishlists.DeleteWishlist>();
            services.AddScoped<CollectionBoundaries.EditWishlistItem.IEditWishlistItemUseCase, CollectionUseCases.Wishlists.EditWishlistItem>();
            services.AddScoped<CollectionBoundaries.GetWishlistById.IGetWishlistByIdUseCase, CollectionUseCases.Wishlists.GetWishlistById>();
            services.AddScoped<CollectionBoundaries.GetWishlistsByOwner.IGetWishlistsByOwnerUseCase, CollectionUseCases.Wishlists.GetWishlistsByOwner>();
            services.AddScoped<CollectionBoundaries.RemoveItemFromWishlist.IRemoveItemFromWishlistUseCase, CollectionUseCases.Wishlists.RemoveItemFromWishlist>();

            services.AddScoped<IWishlistsFactory, WishlistsFactory>();

            return services;
        }
    }
}
