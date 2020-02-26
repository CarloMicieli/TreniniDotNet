using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Web.ViewModels.Links;
using CatalogBoundaries = TreniniDotNet.Application.Boundaries.Catalog;
using CatalogUseCases = TreniniDotNet.Web.UseCases.V1.Catalog;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PresentersExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddSingleton<ILinksGenerator, AspNetLinksGenerator>();

            services.AddBrandPresenters();
            services.AddRailwayPresenters();

            services.AddPresenter<CatalogBoundaries.CreateScale.ICreateScaleOutputPort, CatalogUseCases.CreateScale.CreateScalePresenter>();
            services.AddPresenter<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugOutputPort, CatalogUseCases.GetScaleBySlug.GetScaleBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.GetScalesList.IGetScalesListOutputPort, CatalogUseCases.GetScalesList.GetScalesListPresenter>();

            return services;
        }

        private static IServiceCollection AddBrandPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateBrand.ICreateBrandOutputPort, CatalogUseCases.CreateBrand.CreateBrandPresenter>();
            services.AddPresenter<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugOutputPort, CatalogUseCases.GetBrandBySlug.GetBrandBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.GetBrandsList.IGetBrandsListOutputPort, CatalogUseCases.GetBrandsList.GetBrandsListPresenter>();

            return services;
        }

        private static IServiceCollection AddRailwayPresenters(this IServiceCollection services)
        {
            services.AddPresenter<CatalogBoundaries.CreateRailway.ICreateRailwayOutputPort, CatalogUseCases.CreateRailway.CreateRailwayPresenter>();
            services.AddPresenter<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugOutputPort, CatalogUseCases.GetRailwayBySlug.GetRailwayBySlugPresenter>();
            services.AddPresenter<CatalogBoundaries.GetRailwaysList.IGetRailwaysListOutputPort, CatalogUseCases.GetRailwaysList.GetRailwaysListPresenter>();

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