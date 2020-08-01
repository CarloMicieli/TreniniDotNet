using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Collecting.Collections;
using TreniniDotNet.Infrastructure.Persistence.Collecting.Shops;
using TreniniDotNet.Infrastructure.Persistence.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Images;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddCatalogPersistence();
            services.AddCollectionPersistence();

            services.AddScoped<ImagesRepository>();

            return services;
        }

        private static IServiceCollection AddCatalogPersistence(this IServiceCollection services)
        {
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<ICatalogItemsRepository, CatalogItemsRepository>();
            return services;
        }

        private static IServiceCollection AddCollectionPersistence(this IServiceCollection services)
        {
            services.AddScoped<ICollectionsRepository, CollectionsRepository>();
            services.AddScoped<IWishlistsRepository, WishlistsRepository>();
            services.AddScoped<IShopsRepository, ShopsRepository>();
            return services;
        }
    }
}
