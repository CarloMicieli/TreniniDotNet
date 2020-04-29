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
using TreniniDotNet.Web.Catalog.V1.Brands.CreateBrand;
using TreniniDotNet.Web.Catalog.V1.Brands.EditBrand;
using TreniniDotNet.Web.Catalog.V1.Brands.GetBrandBySlug;
using TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway;
using TreniniDotNet.Web.Catalog.V1.Railways.EditRailway;
using TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug;
using TreniniDotNet.Web.Catalog.V1.Railways.GetRailwaysList;
using TreniniDotNet.Web.Catalog.V1.Scales.CreateScale;
using TreniniDotNet.Web.Catalog.V1.Scales.EditScale;
using TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug;
using TreniniDotNet.Web.Catalog.V1.Scales.GetScalesList;

namespace TreniniDotNet.Web.Catalog.V1
{
    public static class AddCatalogPresentersExtension
    {
        public static IServiceCollection AddCatalogPresenters(this IServiceCollection services)
        {
            services.AddBrandPresenters();
            services.AddRailwayPresenters();
            services.AddScalePresenters();
            services.AddCatalogItemPresenters();

            return services;
        }

        private static IServiceCollection AddCatalogItemPresenters(this IServiceCollection services)
        {
            services.AddPresenter<ICreateCatalogItemOutputPort, CreateCatalogItemPresenter>();
            services.AddPresenter<IGetCatalogItemBySlugOutputPort, GetCatalogItemBySlugPresenter>();
            services.AddPresenter<IEditCatalogItemOutputPort, EditCatalogItemPresenter>();

            return services;
        }

        private static IServiceCollection AddScalePresenters(this IServiceCollection services)
        {
            services.AddPresenter<ICreateScaleOutputPort, CreateScalePresenter>();
            services.AddPresenter<IGetScaleBySlugOutputPort, GetScaleBySlugPresenter>();
            services.AddPresenter<IGetScalesListOutputPort, GetScalesListPresenter>();
            services.AddPresenter<IEditScaleOutputPort, EditScalePresenter>();

            return services;
        }

        private static IServiceCollection AddBrandPresenters(this IServiceCollection services)
        {
            services.AddPresenter<ICreateBrandOutputPort, CreateBrandPresenter>();
            services.AddPresenter<IGetBrandBySlugOutputPort, GetBrandBySlugPresenter>();
            services.AddPresenter<IGetBrandsListOutputPort, GetBrandsListPresenter>();
            services.AddPresenter<IEditBrandOutputPort, EditBrandPresenter>();

            return services;
        }

        private static IServiceCollection AddRailwayPresenters(this IServiceCollection services)
        {
            services.AddPresenter<ICreateRailwayOutputPort, CreateRailwayPresenter>();
            services.AddPresenter<IGetRailwayBySlugOutputPort, GetRailwayBySlugPresenter>();
            services.AddPresenter<IGetRailwaysListOutputPort, GetRailwaysListPresenter>();
            services.AddPresenter<IEditRailwayOutputPort, EditRailwayPresenter>();

            return services;
        }

        private static IServiceCollection AddPresenter<TOutputPort, TPresenter>(this IServiceCollection services)
            where TOutputPort : class
            where TPresenter : class, TOutputPort
        {
            services.AddScoped<TPresenter, TPresenter>();
            services.AddScoped<TOutputPort>(x => x.GetRequiredService<TPresenter>());
            return services;
        }
    }
}