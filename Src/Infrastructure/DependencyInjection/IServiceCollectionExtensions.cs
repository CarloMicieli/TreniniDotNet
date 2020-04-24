using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Collection.Collections;
using TreniniDotNet.Infrastructure.Persistence.Collection.Shared;
using TreniniDotNet.Infrastructure.Persistence.Collection.Shops;
using TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddCatalogPersistence();
            services.AddCollectionPersistence();
            return services;
        }

        private static IServiceCollection AddCatalogPersistence(this IServiceCollection services)
        {
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            return services;
        }

        private static IServiceCollection AddCollectionPersistence(this IServiceCollection services)
        {
            services.AddScoped<ICollectionsRepository, CollectionsRepository>();
            services.AddScoped<ICollectionItemsRepository, CollectionItemsRepository>();
            services.AddScoped<IWishlistItemsRepository, WishlistItemsRepository>();
            services.AddScoped<IWishlistsRepository, WishlistsRepository>();
            services.AddScoped<IShopsRepository, ShopsRepository>();
            services.AddScoped<ICatalogRefsRepository, CatalogRefsRepository>();

            return services;
        }
    }
}
