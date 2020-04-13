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
            services.AddScoped<CollectionBoundaries.AddItemToCollection.IAddItemToCollectionUseCase, CollectionUseCases.AddItemToCollection>();
            services.AddScoped<CollectionBoundaries.EditCollectionItem.IEditCollectionItemUseCase, CollectionUseCases.EditCollectionItem>();
            services.AddScoped<CollectionBoundaries.GetUserCollection.IGetUserCollectionUseCase, CollectionUseCases.GetUserCollection>();
            services.AddScoped<CollectionBoundaries.CreateCollection.ICreateCollectionUseCase, CollectionUseCases.CreateCollection>();
            services.AddScoped<ICollectionsFactory, CollectionsFactory>();

            return services;
        }

        private static IServiceCollection AddShopUseCases(this IServiceCollection services)
        {
            services.AddScoped<CollectionBoundaries.AddShopToFavourites.IAddShopToFavouritesUseCase, CollectionUseCases.AddShopToFavourites>();
            services.AddScoped<CollectionBoundaries.CreateShop.ICreateShopUseCase, CollectionUseCases.CreateShop>();
            services.AddScoped<CollectionBoundaries.RemoveShopFromFavourites.IRemoveShopFromFavouritesUseCase, CollectionUseCases.RemoveShopFromFavourites>();
            services.AddScoped<CollectionBoundaries.GetFavouriteShops.IGetFavouriteShopsUseCase, CollectionUseCases.GetFavouriteShops>();
            services.AddScoped<IShopsFactory, ShopsFactory>();

            return services;
        }

        private static IServiceCollection AddWishListUseCases(this IServiceCollection services)
        {
            services.AddScoped<CollectionBoundaries.AddItemToWishlist.IAddItemToWishlistUseCase, CollectionUseCases.AddItemToWishlist>();
            services.AddScoped<CollectionBoundaries.CreateWishlist.ICreateWishlistUseCase, CollectionUseCases.CreateWishlist>();
            services.AddScoped<CollectionBoundaries.DeleteWishlist.IDeleteWishlistUseCase, CollectionUseCases.DeleteWishlist>();
            services.AddScoped<CollectionBoundaries.EditWishlistItem.IEditWishlistItemUseCase, CollectionUseCases.EditWishlistItem>();
            services.AddScoped<CollectionBoundaries.GetWishlistById.IGetWishlistByIdUseCase, CollectionUseCases.GetWishlistById>();
            services.AddScoped<CollectionBoundaries.GetWishlistsList.IGetWishlistsListUseCase, CollectionUseCases.GetWishlistsList>();
            services.AddScoped<IWishlistsFactory, WishlistsFactory>();

            return services;
        }
    }
}
