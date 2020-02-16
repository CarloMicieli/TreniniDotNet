using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PresentersExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateBrandPresenter, CreateBrandPresenter>();
            services.AddScoped<TreniniDotNet.Application.Boundaries.CreateBrand.IOutputPort>(x => x.GetRequiredService<CreateBrandPresenter>());

            services.AddScoped<GetBrandBySlugPresenter, GetBrandBySlugPresenter>();
            services.AddScoped<TreniniDotNet.Application.Boundaries.GetBrandBySlug.IOutputPort>(x => x.GetRequiredService<GetBrandBySlugPresenter>());

            services.AddScoped<GetScaleBySlugPresenter, GetScaleBySlugPresenter>();
            services.AddScoped<TreniniDotNet.Application.Boundaries.GetScaleBySlug.IOutputPort>(x => x.GetRequiredService<GetScaleBySlugPresenter>());

            return services;
        }
    }
}