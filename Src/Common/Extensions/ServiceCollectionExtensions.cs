using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TreniniDotNet.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterByType(this IServiceCollection services, Type type) =>
            RegisterByType(services, Assembly.GetExecutingAssembly(), type);

        public static IServiceCollection RegisterByType(this IServiceCollection services,
            Assembly assembly,
            Type type)
        {
            assembly
                .GetTypes()
                .Where(item => item.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Any(i => i.GetGenericTypeDefinition() == type) && !item.IsAbstract && !item.IsInterface)
                .ToList()
                .ForEach(assignedTypes =>
                {
                    var serviceType = assignedTypes.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .First(i => i.GetGenericTypeDefinition() == type);
                    services.AddScoped(serviceType, assignedTypes);
                });

            return services;
        }
    }
}
