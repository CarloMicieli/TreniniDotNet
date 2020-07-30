using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.GrpcServices.Catalog.DependencyInjection
{
    public static class CatalogInputValidatorsExtensions
    {
        public static IServiceCollection AddUseCaseInputValidators(this IServiceCollection services)
        {
            services.RegisterByType(typeof(CreateBrandInputValidator).Assembly, typeof(IUseCaseInputValidator<>));
            return services;
        }
    }
}