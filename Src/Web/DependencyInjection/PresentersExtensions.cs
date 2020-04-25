using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Web.ViewModels.Links;
using CatalogBoundaries = TreniniDotNet.Application.Boundaries.Catalog;
using CatalogUseCases = TreniniDotNet.Web.UseCases.V1.Catalog;
using CollectionBoundaries = TreniniDotNet.Application.Boundaries.Collection;
using CollectionUseCases = TreniniDotNet.Web.UseCases.V1.Collection;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PresentersExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddSingleton<ILinksGenerator, AspNetLinksGenerator>();

            services.AddBrandPresenters();
            services.AddRailwayPresenters();
            services.AddScalePresenters();
            services.AddCatalogItemPresenters();

            services.AddCollectionPresenters();
            services.AddWishlistPresenters();
            services.AddShopPresenters();

            return services;
        }

        private static IServiceCollection AddCatalogItemPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateCatalogItem.ICreateCatalogItemOutputPort, CatalogUseCases.CreateCatalogItem.CreateCatalogItemPresenter>();
            services.AddPresenter<CatalogBoundaries.GetCatalogItemBySlug.IGetCatalogItemBySlugOutputPort, CatalogUseCases.GetCatalogItemBySlug.GetCatalogItemBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.EditCatalogItem.IEditCatalogItemOutputPort, CatalogUseCases.EditCatalogItem.EditCatalogItemPresenter>();

            return services;
        }

        private static IServiceCollection AddScalePresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateScale.ICreateScaleOutputPort, CatalogUseCases.CreateScale.CreateScalePresenter>();
            services.AddPresenter<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugOutputPort, CatalogUseCases.GetScaleBySlug.GetScaleBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.GetScalesList.IGetScalesListOutputPort, CatalogUseCases.GetScalesList.GetScalesListPresenter>();
            services.AddPresenter<CatalogBoundaries.EditScale.IEditScaleOutputPort, CatalogUseCases.EditScale.EditScalePresenter>();

            return services;
        }

        private static IServiceCollection AddBrandPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateBrand.ICreateBrandOutputPort, CatalogUseCases.CreateBrand.CreateBrandPresenter>();
            services.AddPresenter<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugOutputPort, CatalogUseCases.GetBrandBySlug.GetBrandBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.GetBrandsList.IGetBrandsListOutputPort, CatalogUseCases.GetBrandsList.GetBrandsListPresenter>();
            services.AddPresenter<CatalogBoundaries.EditBrand.IEditBrandOutputPort, CatalogUseCases.EditBrand.EditBrandPresenter>();

            return services;
        }

        private static IServiceCollection AddRailwayPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateRailway.ICreateRailwayOutputPort, CatalogUseCases.CreateRailway.CreateRailwayPresenter>();
            services.AddPresenter<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugOutputPort, CatalogUseCases.GetRailwayBySlug.GetRailwayBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.GetRailwaysList.IGetRailwaysListOutputPort, CatalogUseCases.GetRailwaysList.GetRailwaysListPresenter>();
            services.AddPresenter<CatalogBoundaries.EditRailway.IEditRailwayOutputPort, CatalogUseCases.EditRailway.EditRailwayPresenter>();

            return services;
        }

        private static IServiceCollection AddCollectionPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CollectionBoundaries.AddItemToCollection.IAddItemToCollectionOutputPort, CollectionUseCases.AddItemToCollection.AddItemToCollectionPresenter>();
            services.AddPresenter<CollectionBoundaries.CreateCollection.ICreateCollectionOutputPort, CollectionUseCases.CreateCollection.CreateCollectionPresenter>();
            services.AddPresenter<CollectionBoundaries.EditCollectionItem.IEditCollectionItemOutputPort, CollectionUseCases.EditCollectionItem.EditCollectionItemPresenter>();
            services.AddPresenter<CollectionBoundaries.GetCollectionByOwner.IGetCollectionByOwnerOutputPort, CollectionUseCases.GetCollectionByOwner.GetCollectionByOwnerPresenter>();
            services.AddPresenter<CollectionBoundaries.RemoveItemFromCollection.IRemoveItemFromCollectionOutputPort, CollectionUseCases.RemoveItemFromCollection.RemoveItemFromCollectionPresenter>();
            services.AddPresenter<CollectionBoundaries.GetCollectionStatistics.IGetCollectionStatisticsOutputPort, CollectionUseCases.GetCollectionStatistics.GetCollectionStatisticsPresenter>();

            return services;
        }

        private static IServiceCollection AddWishlistPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CollectionBoundaries.AddItemToWishlist.IAddItemToWishlistOutputPort, CollectionUseCases.AddItemToWishlist.AddItemToWishlistPresenter>();
            services.AddPresenter<CollectionBoundaries.CreateWishlist.ICreateWishlistOutputPort, CollectionUseCases.CreateWishlist.CreateWishlistPresenter>();
            services.AddPresenter<CollectionBoundaries.DeleteWishlist.IDeleteWishlistOutputPort, CollectionUseCases.DeleteWishlist.DeleteWishlistPresenter>();
            services.AddPresenter<CollectionBoundaries.EditWishlistItem.IEditWishlistItemOutputPort, CollectionUseCases.EditWishlistItem.EditWishlistItemPresenter>();
            services.AddPresenter<CollectionBoundaries.GetWishlistById.IGetWishlistByIdOutputPort, CollectionUseCases.GetWishlistById.GetWishlistByIdPresenter>();
            services.AddPresenter<CollectionBoundaries.GetWishlistsByOwner.IGetWishlistsByOwnerOutputPort, CollectionUseCases.GetWishlistsByOwner.GetWishlistsByOwnerPresenter>();
            services.AddPresenter<CollectionBoundaries.RemoveItemFromWishlist.IRemoveItemFromWishlistOutputPort, CollectionUseCases.RemoveItemFromWishlist.RemoveItemFromWishlistPresenter>();

            return services;
        }

        private static IServiceCollection AddShopPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CollectionBoundaries.AddShopToFavourites.IAddShopToFavouritesOutputPort, CollectionUseCases.AddShopToFavourites.AddShopToFavouritesPresenter>();
            services.AddPresenter<CollectionBoundaries.CreateShop.ICreateShopOutputPort, CollectionUseCases.CreateShop.CreateShopPresenter>();
            services.AddPresenter<CollectionBoundaries.GetFavouriteShops.IGetFavouriteShopsOutputPort, CollectionUseCases.GetFavouriteShops.GetFavouriteShopsPresenter>();
            services.AddPresenter<CollectionBoundaries.RemoveShopFromFavourites.IRemoveShopFromFavouritesOutputPort, CollectionUseCases.RemoveShopFromFavourites.RemoveShopFromFavouritesPresenter>();
            services.AddPresenter<CollectionBoundaries.GetShopBySlug.IGetShopBySlugOutputPort, CollectionUseCases.GetShopBySlug.GetShopBySlugPresenter>();
            services.AddPresenter<CollectionBoundaries.GetShopsList.IGetShopsListOutputPort, CollectionUseCases.GetShopsList.GetShopsListPresenter>();

            return services;
        }

        private static IServiceCollection AddPresenter<TOutputPort, TPresenter>(this IServiceCollection services)
            where TOutputPort : class
            where TPresenter : class, TOutputPort
        {
            services.AddScoped<TPresenter, TPresenter>();
            services.AddScoped<TOutputPort>(x => x.GetRequiredService<TPresenter>());
            return services;
        }
    }
}