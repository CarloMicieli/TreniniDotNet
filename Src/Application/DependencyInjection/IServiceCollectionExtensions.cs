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
            services.AddScoped<CatalogBoundaries.CreateBrand.ICreateBrandUseCase, CatalogUseCases.Brands.CreateBrand>();
            services.AddScoped<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugUseCase, CatalogUseCases.Brands.GetBrandBySlug>();
            services.AddScoped<CatalogBoundaries.GetBrandsList.IGetBrandsListUseCase, CatalogUseCases.Brands.GetBrandsList>();
            services.AddScoped<CatalogBoundaries.EditBrand.IEditBrandUseCase, CatalogUseCases.Brands.EditBrand>();

            services.AddScoped<IBrandsFactory, BrandsFactory>();
            services.AddScoped<BrandService>();
            return services;
        }

        private static IServiceCollection AddScaleUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateScale.ICreateScaleUseCase, CatalogUseCases.Scales.CreateScale>();
            services.AddScoped<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugUseCase, CatalogUseCases.Scales.GetScaleBySlug>();
            services.AddScoped<CatalogBoundaries.GetScalesList.IGetScalesListUseCase, CatalogUseCases.Scales.GetScalesList>();
            services.AddScoped<CatalogBoundaries.EditScale.IEditScaleUseCase, CatalogUseCases.Scales.EditScale>();

            services.AddScoped<IScalesFactory, ScalesFactory>();
            services.AddScoped<ScaleService>();
            return services;
        }

        private static IServiceCollection AddRailwayUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateRailway.ICreateRailwayUseCase, CatalogUseCases.Railways.CreateRailway>();
            services.AddScoped<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugUseCase, CatalogUseCases.Railways.GetRailwayBySlug>();
            services.AddScoped<CatalogBoundaries.GetRailwaysList.IGetRailwaysListUseCase, CatalogUseCases.Railways.GetRailwaysList>();
            services.AddScoped<CatalogBoundaries.EditRailway.IEditRailwayUseCase, CatalogUseCases.Railways.EditRailway>();

            services.AddScoped<IRailwaysFactory, RailwaysFactory>();
            services.AddScoped<RailwayService>();
            return services;
        }

        private static IServiceCollection AddCatalogItemUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateCatalogItem.ICreateCatalogItemUseCase, CatalogUseCases.CatalogItems.CreateCatalogItem>();
            services.AddScoped<CatalogBoundaries.GetCatalogItemBySlug.IGetCatalogItemBySlugUseCase, CatalogUseCases.CatalogItems.GetCatalogItemBySlug>();
            services.AddScoped<CatalogBoundaries.EditCatalogItem.IEditCatalogItemUseCase, CatalogUseCases.CatalogItems.EditCatalogItem>();

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
            services.AddScoped<CollectionsService>();
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
            services.AddScoped<ShopsService>();
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
            services.AddScoped<WishlistService>();
            return services;
        }
    }
}
