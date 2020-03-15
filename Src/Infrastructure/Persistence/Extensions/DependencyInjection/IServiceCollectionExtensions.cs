using Microsoft.Extensions.DependencyInjection;

namespace TreniniDotNet.Infrastracture.Persistence.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
