using Microsoft.Extensions.DependencyInjection;

using CatalogBoundaries = TreniniDotNet.Application.Boundaries.Catalog;
using CatalogUseCases = TreniniDotNet.Web.UseCases.V1.Catalog;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PresentersExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateBrand.ICreateBrandOutputPort, CatalogUseCases.CreateBrand.CreateBrandPresenter>();
            services.AddPresenter<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugOutputPort, CatalogUseCases.GetBrandBySlug.GetBrandBySlugPresenter>();
            
            services.AddPresenter<CatalogBoundaries.CreateScale.ICreateScaleOutputPort, CatalogUseCases.CreateScale.CreateScalePresenter>();
            services.AddPresenter<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugOutputPort, CatalogUseCases.GetScaleBySlug.GetScaleBySlugPresenter>();

            services.AddPresenter<CatalogBoundaries.CreateRailway.ICreateRailwayOutputPort, CatalogUseCases.CreateRailway.CreateRailwayPresenter>();
            services.AddPresenter<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugOutputPort, CatalogUseCases.GetRailwayBySlug.GetRailwayBySlugPresenter>();

            return services;
        }

        private static IServiceCollection AddPresenter<TOutputPort, TPresenter>(this IServiceCollection services)
            where TOutputPort: class
            where TPresenter: class, TOutputPort
        {
            services.AddScoped<TPresenter, TPresenter>();
            services.AddScoped<TOutputPort>(x => x.GetRequiredService<TPresenter>());
            return services;
        }
    }
}