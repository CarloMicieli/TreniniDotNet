using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PersistanceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddCatalogPersistence();
            return services;
        }

        private static IServiceCollection AddCatalogPersistence(this IServiceCollection services)
        {
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<IBrandsFactory, BrandsFactory>();

            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IRailwaysFactory, Domain.Catalog.Railways.RailwaysFactory>();

            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<IScalesFactory, Domain.Catalog.Scales.ScalesFactory>();

            services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            services.AddScoped<ICatalogItemsFactory, CatalogItemsFactory>();

            return services;
        }
    }
}

