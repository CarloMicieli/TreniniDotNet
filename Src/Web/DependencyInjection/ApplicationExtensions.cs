using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<TreniniDotNet.Application.Boundaries.Catalog.CreateBrand.ICreateBrandUseCase, TreniniDotNet.Application.UseCases.Catalog.CreateBrand>();
            services.AddScoped<TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug.IGetBrandBySlugUseCase, TreniniDotNet.Application.UseCases.Catalog.GetBrandBySlug>();
            services.AddScoped<TreniniDotNet.Application.Boundaries.Catalog.CreateScale.ICreateScaleUseCase, TreniniDotNet.Application.UseCases.Catalog.CreateScale>();

            services.AddScoped<BrandService>();
            services.AddScoped<ScaleService>();

            services.AddSingleton(typeof(IUseCaseInputValidator<>), typeof(UseCaseInputValidator<>));

            return services;
        }
    }
}
