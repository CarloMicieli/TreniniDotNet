using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.GrpcServices.Catalog.Brands;
using TreniniDotNet.GrpcServices.Catalog.Railways;
using TreniniDotNet.GrpcServices.Catalog.Scales;

namespace TreniniDotNet.GrpcServices.Catalog.DependencyInjection
{
    public static class CatalogPresentersExtension
    {
        public static IServiceCollection AddCatalogPresenters(this IServiceCollection services)
        {
            services.AddPresenter<ICreateBrandOutputPort, CreateBrandPresenter>();
            services.AddPresenter<ICreateRailwayOutputPort, CreateRailwayPresenter>();
            services.AddPresenter<ICreateScaleOutputPort, CreateScalePresenter>();
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
