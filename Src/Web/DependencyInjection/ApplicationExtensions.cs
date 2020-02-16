using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<TreniniDotNet.Application.Boundaries.CreateBrand.IUseCase, TreniniDotNet.Application.UseCases.Catalog.CreateBrand>();
            services.AddScoped<TreniniDotNet.Application.Boundaries.GetBrandBySlug.IUseCase, TreniniDotNet.Application.UseCases.Catalog.GetBrandBySlug>();
            
            services.AddScoped<BrandService>();
            
            return services;
        }
    }
}
