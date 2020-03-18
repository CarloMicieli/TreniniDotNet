using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Infrastracture.Dapper;
using TreniniDotNet.Infrastracture.Dapper.Extensions.DependencyInjection;
using static Dapper.SqlMapper;

namespace TreniniDotNet.Infrastracture.Extensions.DependencyInjection
{
    public static class DapperExtensions
    {
        public static IServiceCollection ReplaceDapper(this IServiceCollection services, Action<DapperOptions> action)
        {
            var descriptors = services
                .Where(d => d.ServiceType == typeof(IDatabaseContext) || d.ServiceType == typeof(IUnitOfWork))
                .ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            return services.AddDapper(action);
        }

        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DapperOptions> action)
        {
            var options = new DapperOptions(services);
            action?.Invoke(options);

            Type baseType = typeof(ITypeHandler);

            var typeHandlers = options.Assemblies
                .SelectMany(a =>
                    a.GetTypes()
                     .Where(t => baseType.IsAssignableFrom(t)));

            foreach (var typeHandler in typeHandlers)
            {
                var iTypeHandler = Activator.CreateInstance(typeHandler) as ITypeHandler;
                var type = typeHandler?.BaseType?.GetGenericArguments()[0];
                if (type != null && iTypeHandler != null)
                {
                    SqlMapper.AddTypeHandler(type, iTypeHandler);
                }
            }

            services.AddScoped<IUnitOfWork, DapperUnitOfWork>();

            return services;
        }
    }
}
