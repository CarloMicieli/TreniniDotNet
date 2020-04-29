using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Application.Collecting.Collections.CreateCollection;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;
using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;
using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;
using TreniniDotNet.Application.Collecting.Shops.GetShopBySlug;
using TreniniDotNet.Application.Collecting.Shops.GetShopsList;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCollectingUseCases(this IServiceCollection services)
        {
            services.AddCollectionUseCases();
            services.AddShopUseCases();
            services.AddWishListUseCases();
            return services;
        }

        private static IServiceCollection AddCollectionUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAddItemToCollectionUseCase, AddItemToCollectionUseCase>();
            services.AddScoped<IEditCollectionItemUseCase, EditCollectionItemUseCase>();
            services.AddScoped<IGetCollectionByOwnerUseCase, GetCollectionByOwnerUseCase>();
            services.AddScoped<ICreateCollectionUseCase, CreateCollectionUseCase>();
            services.AddScoped<IRemoveItemFromCollectionUseCase, RemoveItemFromCollectionUseCase>();
            services.AddScoped<IGetCollectionStatisticsUseCase, GetCollectionStatisticsUseCase>();

            services.AddScoped<ICollectionsFactory, CollectionsFactory>();
            services.AddScoped<CollectionsService>();
            return services;
        }

        private static IServiceCollection AddShopUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAddShopToFavouritesUseCase, AddShopToFavouritesUseCase>();
            services.AddScoped<ICreateShopUseCase, CreateShopUseCase>();
            services.AddScoped<IRemoveShopFromFavouritesUseCase, RemoveShopFromFavouritesUseCase>();
            services.AddScoped<IGetFavouriteShopsUseCase, GetFavouriteShopsUseCase>();
            services.AddScoped<IGetShopBySlugUseCase, GetShopBySlugUseCase>();
            services.AddScoped<IGetShopsListUseCase, GetShopsListUseCase>();

            services.AddScoped<IShopsFactory, ShopsFactory>();
            services.AddScoped<ShopsService>();
            return services;
        }

        private static IServiceCollection AddWishListUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAddItemToWishlistUseCase, AddItemToWishlistUseCase>();
            services.AddScoped<ICreateWishlistUseCase, CreateWishlistUseCase>();
            services.AddScoped<IDeleteWishlistUseCase, DeleteWishlistUseCase>();
            services.AddScoped<IEditWishlistItemUseCase, EditWishlistItemUseCase>();
            services.AddScoped<IGetWishlistByIdUseCase, GetWishlistByIdUseCase>();
            services.AddScoped<IGetWishlistsByOwnerUseCase, GetWishlistsByOwnerUseCase>();
            services.AddScoped<IRemoveItemFromWishlistUseCase, RemoveItemFromWishlistUseCase>();

            services.AddScoped<IWishlistsFactory, WishlistsFactory>();
            services.AddScoped<WishlistService>();
            return services;
        }
    }
}