using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Catalog;

namespace TreniniDotNet.GrpcServices.Catalog.DependencyInjection
{
    public static class CatalogServicesExtension
    {
        public static IServiceCollection AddCatalogServices(this IServiceCollection services)
        {
            services.AddBrandService();
            services.AddCatalogItemService();
            services.AddRailwayService();
            services.AddScaleService();
            return services;
        }

        private static IServiceCollection AddBrandService(this IServiceCollection services)
        {
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<BrandsFactory>();
            services.AddScoped<BrandsService>();
            return services;
        }

        private static IServiceCollection AddCatalogItemService(this IServiceCollection services)
        {
            services.AddScoped<ICatalogItemsRepository, CatalogItemsRepository>();
            services.AddScoped<CatalogItemsFactory>();
            services.AddScoped<RollingStocksFactory>();
            services.AddScoped<CatalogItemsService>();
            return services;
        }

        private static IServiceCollection AddRailwayService(this IServiceCollection services)
        {
            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<RailwaysFactory>();
            services.AddScoped<RailwaysService>();
            return services;
        }

        private static IServiceCollection AddScaleService(this IServiceCollection services)
        {
            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<ScalesFactory>();
            services.AddScoped<ScalesService>();
            return services;
        }
    }
}
