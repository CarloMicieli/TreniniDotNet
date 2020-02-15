using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug;

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

            return services;
        }
    }
}