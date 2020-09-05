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
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlist;
using TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistById;
using TreniniDotNet.Application.Collecting.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist;
using TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection;
using TreniniDotNet.Web.Collecting.V1.Collections.CreateCollection;
using TreniniDotNet.Web.Collecting.V1.Collections.EditCollectionItem;
using TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionByOwner;
using TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionStatistics;
using TreniniDotNet.Web.Collecting.V1.Collections.RemoveItemFromCollection;
using TreniniDotNet.Web.Collecting.V1.Shops.AddShopToFavourites;
using TreniniDotNet.Web.Collecting.V1.Shops.CreateShop;
using TreniniDotNet.Web.Collecting.V1.Shops.GetFavouriteShops;
using TreniniDotNet.Web.Collecting.V1.Shops.GetShopBySlug;
using TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList;
using TreniniDotNet.Web.Collecting.V1.Shops.RemoveShopFromFavourites;
using TreniniDotNet.Web.Collecting.V1.Wishlists.AddItemToWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.CreateWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.DeleteWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlist;
using TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlistItem;
using TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistById;
using TreniniDotNet.Web.Collecting.V1.Wishlists.GetWishlistsByOwner;
using TreniniDotNet.Web.Collecting.V1.Wishlists.RemoveItemFromWishlist;

namespace TreniniDotNet.Web.Collecting.V1
{
    public static class AddCollectingPresentersExtension
    {
        public static IServiceCollection AddCollectingPresenter(this IServiceCollection services)
        {
            services.AddCollectionPresenters();
            services.AddWishlistPresenters();
            services.AddShopPresenters();

            return services;
        }

        private static IServiceCollection AddCollectionPresenters(this IServiceCollection services)
        {
            services.AddPresenter<IAddItemToCollectionOutputPort, AddItemToCollectionPresenter>();
            services.AddPresenter<ICreateCollectionOutputPort, CreateCollectionPresenter>();
            services.AddPresenter<IEditCollectionItemOutputPort, EditCollectionItemPresenter>();
            services.AddPresenter<IGetCollectionByOwnerOutputPort, GetCollectionByOwnerPresenter>();
            services.AddPresenter<IRemoveItemFromCollectionOutputPort, RemoveItemFromCollectionPresenter>();
            services.AddPresenter<IGetCollectionStatisticsOutputPort, GetCollectionStatisticsPresenter>();

            return services;
        }

        private static IServiceCollection AddWishlistPresenters(this IServiceCollection services)
        {
            services.AddPresenter<IAddItemToWishlistOutputPort, AddItemToWishlistPresenter>();
            services.AddPresenter<ICreateWishlistOutputPort, CreateWishlistPresenter>();
            services.AddPresenter<IDeleteWishlistOutputPort, DeleteWishlistPresenter>();
            services.AddPresenter<IEditWishlistOutputPort, EditWishlistPresenter>();
            services.AddPresenter<IEditWishlistItemOutputPort, EditWishlistItemPresenter>();
            services.AddPresenter<IGetWishlistByIdOutputPort, GetWishlistByIdPresenter>();
            services.AddPresenter<IGetWishlistsByOwnerOutputPort, GetWishlistsByOwnerPresenter>();
            services.AddPresenter<IRemoveItemFromWishlistOutputPort, RemoveItemFromWishlistPresenter>();

            return services;
        }

        private static IServiceCollection AddShopPresenters(this IServiceCollection services)
        {
            services.AddPresenter<IAddShopToFavouritesOutputPort, AddShopToFavouritesPresenter>();
            services.AddPresenter<ICreateShopOutputPort, CreateShopPresenter>();
            services.AddPresenter<IGetFavouriteShopsOutputPort, GetFavouriteShopsPresenter>();
            services.AddPresenter<IRemoveShopFromFavouritesOutputPort, RemoveShopFromFavouritesPresenter>();
            services.AddPresenter<IGetShopBySlugOutputPort, GetShopBySlugPresenter>();
            services.AddPresenter<IGetShopsListOutputPort, GetShopsListPresenter>();

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
