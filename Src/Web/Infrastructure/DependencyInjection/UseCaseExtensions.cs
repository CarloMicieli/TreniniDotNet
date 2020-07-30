using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Web.Infrastructure.DependencyInjection
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.RegisterByType(typeof(BrandsService).Assembly, typeof(IFactory<,>));
            services.RegisterByType(typeof(CreateBrandUseCase).Assembly, typeof(IUseCase<>));
            services.RegisterByType(typeof(CreateBrandInputValidator).Assembly, typeof(IUseCaseInputValidator<>));

            return services;
        }
    }
}
