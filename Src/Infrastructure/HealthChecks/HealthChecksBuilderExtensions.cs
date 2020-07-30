using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Infrastructure.Persistence;

namespace TreniniDotNet.Infrastructure.HealthChecks
{
    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddDatabaseHealthChecks(this IHealthChecksBuilder builder)
        {
            return builder.AddDbContextCheck<ApplicationDbContext>("DbHealthCheck"); ;
        }
    }
}
