using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.Railways;

using CatalogUseCases = TreniniDotNet.Application.UseCases.Catalog;
using CatalogBoundaries = TreniniDotNet.Application.Boundaries.Catalog;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddBrandUseCases();
            services.AddScaleUseCases();
            services.AddRailwayUseCases();
            services.AddCatalogItemUseCases();

            services.AddScoped(typeof(IUseCaseInputValidator<>), typeof(UseCaseInputValidator<>));

            return services;
        }

        private static IServiceCollection AddBrandUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateBrand.ICreateBrandUseCase, CatalogUseCases.CreateBrand>();
            services.AddScoped<CatalogBoundaries.GetBrandBySlug.IGetBrandBySlugUseCase, CatalogUseCases.GetBrandBySlug>();
            services.AddScoped<CatalogBoundaries.GetBrandsList.IGetBrandsListUseCase, CatalogUseCases.GetBrandsList>();
            services.AddScoped<IBrandsFactory, BrandsFactory>();

            services.AddScoped<BrandService>();

            return services;
        }

        private static IServiceCollection AddScaleUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateScale.ICreateScaleUseCase, CatalogUseCases.CreateScale>();
            services.AddScoped<CatalogBoundaries.GetScaleBySlug.IGetScaleBySlugUseCase, CatalogUseCases.GetScaleBySlug>();
            services.AddScoped<CatalogBoundaries.GetScalesList.IGetScalesListUseCase, CatalogUseCases.GetScalesList>();
            services.AddScoped<IScalesFactory, ScalesFactory>();

            services.AddScoped<ScaleService>();

            return services;
        }

        private static IServiceCollection AddRailwayUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateRailway.ICreateRailwayUseCase, CatalogUseCases.CreateRailway>();
            services.AddScoped<CatalogBoundaries.GetRailwayBySlug.IGetRailwayBySlugUseCase, CatalogUseCases.GetRailwayBySlug>();
            services.AddScoped<CatalogBoundaries.GetRailwaysList.IGetRailwaysListUseCase, CatalogUseCases.GetRailwaysList>();
            services.AddScoped<IRailwaysFactory, RailwaysFactory>();

            services.AddScoped<RailwayService>();

            return services;
        }

        private static IServiceCollection AddCatalogItemUseCases(this IServiceCollection services)
        {
            services.AddScoped<CatalogBoundaries.CreateCatalogItem.ICreateCatalogItemUseCase, CatalogUseCases.CreateCatalogItem>();
            services.AddScoped<CatalogBoundaries.GetCatalogItemBySlug.IGetCatalogItemBySlugUseCase, CatalogUseCases.GetCatalogItemBySlug>();
            services.AddScoped<ICatalogItemsFactory, CatalogItemsFactory>();

            services.AddScoped<CatalogItemService>();

            return services;
        }
    }
}
