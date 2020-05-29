using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.GrpcServices.Catalog.Brands;
using TreniniDotNet.GrpcServices.Catalog.CatalogItems;
using TreniniDotNet.GrpcServices.Catalog.Railways;
using TreniniDotNet.GrpcServices.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

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
            services.AddScoped<IBrandsFactory, BrandsFactory>();
            services.AddScoped<BrandService>();
            return services;
        }

        private static IServiceCollection AddCatalogItemService(this IServiceCollection services)
        {
            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<ICatalogItemsFactory, CatalogItemsFactory>();
            services.AddScoped<IRollingStocksFactory, RollingStocksFactory>();
            services.AddScoped<CatalogItemService>();
            return services;
        }

        private static IServiceCollection AddRailwayService(this IServiceCollection services)
        {
            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IRailwaysFactory, RailwaysFactory>();
            services.AddScoped<RailwayService>();
            return services;
        }

        private static IServiceCollection AddScaleService(this IServiceCollection services)
        {
            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<IScalesFactory, ScalesFactory>();
            services.AddScoped<ScaleService>();
            return services;
        }
    }
}
