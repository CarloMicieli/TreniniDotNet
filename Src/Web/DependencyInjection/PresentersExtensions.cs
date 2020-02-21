using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PresentersExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddPresenter<TreniniDotNet.Application.Boundaries.Catalog.CreateBrand.ICreateBrandOutputPort, CreateBrandPresenter>();
            services.AddPresenter<TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug.IOutputPort, GetBrandBySlugPresenter>();
            services.AddPresenter<TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug.IOutputPort, GetScaleBySlugPresenter>();
            services.AddPresenter<TreniniDotNet.Application.Boundaries.Catalog.CreateScale.IOutputPort, CreateScalePresenter>();
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