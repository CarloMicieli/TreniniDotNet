using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;

namespace TreniniDotNet.GrpcServices.Catalog.DependencyInjection
{
    public static class CatalogUseCasesExtensions
    {
        public static IServiceCollection AddCatalogUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateBrandUseCase>();
            services.AddScoped<CreateCatalogItemUseCase>();
            services.AddScoped<CreateRailwayUseCase>();
            services.AddScoped<CreateScaleUseCase>();
            return services;
        }
    }
}
