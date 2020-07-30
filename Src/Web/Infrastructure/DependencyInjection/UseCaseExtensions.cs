using System.Linq;
using System.Reflection;
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
            services.RegisterByType(typeof(BrandsService).Assembly, typeof(IFactory<,>), false);
            services.RegisterByType(typeof(CreateBrandUseCase).Assembly, typeof(IUseCase<>), false);
            services.RegisterByType(typeof(CreateBrandInputValidator).Assembly, typeof(IUseCaseInputValidator<>));

            services.RegisterDomainServices(typeof(BrandsService).Assembly);
            
            return services;
        }

        public static IServiceCollection RegisterDomainServices(this IServiceCollection services, Assembly assembly)
        {
            var domainServices = assembly.GetTypes()
                .Where(it => !it.IsAbstract && !it.IsInterface)
                .Where(it => it.GetInterfaces().Any(i => i == typeof(IDomainService)))
                .ToList();

            foreach (var domainService in domainServices)
            {
                services.AddScoped(domainService);
            }

            return services;
        }
    }
}
