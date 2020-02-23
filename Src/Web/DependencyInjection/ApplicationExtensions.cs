using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;

using CatalogUseCases = TreniniDotNet.Application.UseCases.Catalog;
using CatalogBoundaries = TreniniDotNet.Application.Boundaries.Catalog;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddBrandUseCases();
            services.AddScaleUseCases();
            services.AddRailwayUseCases();

            services.AddScoped(typeof(IUseCaseInputValidator<>), typeof(UseCaseInputValidator<>));

            return services;
        }

        private static IServiceCollection AddBrandUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateBrand.ICreateBrandUseCase, CatalogUseCases.CreateBrand>();
            services.AddScoped<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugUseCase, CatalogUseCases.GetBrandBySlug>();

            services.AddScoped<BrandService>();

            return services;
        }

        private static IServiceCollection AddScaleUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateScale.ICreateScaleUseCase, CatalogUseCases.CreateScale>();
            services.AddScoped<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugUseCase, CatalogUseCases.GetScaleBySlug>();

            services.AddScoped<ScaleService>();

            return services;
        }

        private static IServiceCollection AddRailwayUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateRailway.ICreateRailwayUseCase, CatalogUseCases.CreateRailway>();
            services.AddScoped<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugUseCase, CatalogUseCases.GetRailwayBySlug>();

            services.AddScoped<RailwayService>();

            return services;
        }
    }
}
