using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug;
using TreniniDotNet.Application.Catalog.Brands.GetBrandsList;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug;
using TreniniDotNet.Application.Catalog.Railways.GetRailwaysList;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug;
using TreniniDotNet.Application.Catalog.Scales.GetScalesList;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogUseCases(this IServiceCollection services)
        {
            services.AddBrandUseCases();
            services.AddCatalogItemUseCases();
            services.AddRailwayUseCases();
            services.AddScaleUseCases();

            return services;
        }

        private static IServiceCollection AddBrandUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateBrandUseCase, CreateBrandUseCase>();
            services.AddScoped<IGetBrandBySlugUseCase, GetBrandBySlugUseCase>();
            services.AddScoped<IGetBrandsListUseCase, GetBrandsListUseCase>();
            services.AddScoped<IEditBrandUseCase, EditBrandUseCase>();

            services.AddScoped<IBrandsFactory, BrandsFactory>();
            services.AddScoped<BrandService>();
            return services;
        }

        private static IServiceCollection AddCatalogItemUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateCatalogItemUseCase, CreateCatalogItemUseCase>();
            services.AddScoped<IGetCatalogItemBySlugUseCase, GetCatalogItemBySlugUseCase>();
            services.AddScoped<IEditCatalogItemUseCase, EditCatalogItemUseCase>();

            services.AddScoped<ICatalogItemsFactory, CatalogItemsFactory>();
            services.AddScoped<CatalogItemService>();
            return services;
        }

        private static IServiceCollection AddRailwayUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateRailwayUseCase, CreateRailwayUseCase>();
            services.AddScoped<IGetRailwayBySlugUseCase, GetRailwayBySlugUseCase>();
            services.AddScoped<IGetRailwaysListUseCase, GetRailwaysListUseCase>();
            services.AddScoped<IEditRailwayUseCase, EditRailwayUseCase>();

            services.AddScoped<IRailwaysFactory, RailwaysFactory>();
            services.AddScoped<RailwayService>();
            return services;
        }

        private static IServiceCollection AddScaleUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateScaleUseCase, CreateScaleUseCase>();
            services.AddScoped<IGetScaleBySlugUseCase, GetScaleBySlugUseCase>();
            services.AddScoped<IGetScalesListUseCase, GetScalesListUseCase>();
            services.AddScoped<IEditScaleUseCase, EditScaleUseCase>();

            services.AddScoped<IScalesFactory, ScalesFactory>();
            services.AddScoped<ScaleService>();
            return services;
        }
    }
}