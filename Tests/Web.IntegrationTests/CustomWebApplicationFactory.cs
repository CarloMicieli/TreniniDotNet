using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TreniniDotNet.IntegrationTests.Helpers.Data;
using System.IO;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Migrations;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using TreniniDotNet.Web.UserProfiles.Identity;

namespace TreniniDotNet.IntegrationTests
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    // ref https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/test/integration-tests/samples/3.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/CustomWebApplicationFactory.cs
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IDisposable
        where TStartup : class
    {
        private readonly Guid contextId;

        public CustomWebApplicationFactory()
        {
            this.contextId = Guid.NewGuid();
        }

        public new void Dispose()
        {
            File.Delete($"{contextId}.db");
            base.Dispose();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("testing");

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();

                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);

                config.AddEnvironmentVariables();
            });

            builder.ConfigureServices(services =>
            {
                services.ReplaceWithInMemory<ApplicationIdentityDbContext>("IdentityInMemoryDatabase");

                string connectionString = $"Data Source={contextId}.db";

                // Replace with sqlite
                services.ReplaceDapper(options =>
                {
                    options.UseSqlite(connectionString);
                    options.ScanTypeHandlersIn(typeof(GuidTypeHandler).Assembly);
                });

                services.ReplaceMigrations(options =>
                {
                    options.UseSqlite(connectionString);
                    options.ScanMigrationsIn(typeof(InitialMigration).Assembly);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                    var migration = scopedServices.GetRequiredService<IDatabaseMigration>();
                    migration.Up();

                    try
                    {
                        // Seed the database with test data.
                        ApplicationContextSeed.SeedCatalog(scopedServices);
                        ApplicationContextSeed.SeedCollections(scopedServices).Wait();
                        AppIdentityDbContextSeed.SeedAsync(userManager, roleManager).Wait();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
#pragma warning restore CA1063 // Implement IDisposable Correctly

    public static class IServiceCollectionTestExtensions
    {
        public static IServiceCollection ReplaceWithInMemory<TContext>(this IServiceCollection services, string databaseName)
            where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add ApplicationDbContext using an in-memory database for testing.
            services.AddDbContext<TContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName);
            });

            return services;
        }
    }
}