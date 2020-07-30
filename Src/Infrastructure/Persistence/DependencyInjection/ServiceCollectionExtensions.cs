using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Catalog;
using TreniniDotNet.Infrastructure.Persistence.Collecting;
using TreniniDotNet.Infrastructure.Persistence.Images;

namespace TreniniDotNet.Infrastructure.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<ICatalogItemsRepository, CatalogItemsRepository>();
            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<ICollectionsRepository, CollectionsRepository>();
            services.AddScoped<IShopsRepository, ShopsRepository>();
            services.AddScoped<IWishlistsRepository, WishlistsRepository>();
            services.AddScoped<ImagesRepository>();

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

            return services;
        }
    }
}
