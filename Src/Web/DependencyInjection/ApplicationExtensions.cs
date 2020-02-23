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
            services.AddScoped<CatalogBoundaries.CreateBrand.ICreateBrandUseCase, CatalogUseCases.CreateBrand>();
            services.AddScoped<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugUseCase, CatalogUseCases.GetBrandBySlug>();
            services.AddScoped<CatalogBoundaries.CreateScale.ICreateScaleUseCase, CatalogUseCases.CreateScale>();
            services.AddScoped<CatalogBoundaries.CreateRailway.ICreateRailwayUseCase, CatalogUseCases.CreateRailway>();

            services.AddScoped<BrandService>();
            services.AddScoped<RailwayService>();
            services.AddScoped<ScaleService>();

            services.AddSingleton(typeof(IUseCaseInputValidator<>), typeof(UseCaseInputValidator<>));

            return services;
        }
    }
}
