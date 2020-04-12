using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddCatalogPersistence();
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
    }
}
