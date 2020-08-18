using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Infrastructure.Identity;

namespace TreniniDotNet.Infrastructure.HealthChecks
{
    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddDatabaseHealthChecks(this IHealthChecksBuilder builder)
        {
            return builder.AddDbContextCheck<ApplicationIdentityDbContext>("DbHealthCheck"); ;
        }
    }
}
