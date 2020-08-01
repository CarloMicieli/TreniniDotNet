using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Infrastructure.Identity;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.DependencyInjection;
using TreniniDotNet.Infrastructure.Persistence.Migrations;
using TreniniDotNet.IntegrationTests.Helpers.Data;

namespace TreniniDotNet.IntegrationTests
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public sealed class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly InMemoryDatabaseRoot _dbRoot = new InMemoryDatabaseRoot();
        private readonly Guid _contextId;

        public CustomWebApplicationFactory()
        {
            _contextId = Guid.NewGuid();
        }
        
        public new void Dispose()
        {
            File.Delete($"{_contextId}.db");
            base.Dispose();
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            
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
                var connectionString = $"Data Source={_contextId}.db";
                
                services.ReplaceWithSqlite<ApplicationDbContext>(connectionString);

                services.ReplaceMigrations(options =>
                {
                    options.UseSqlite(connectionString);
                    options.ScanMigrationsIn(typeof(InitialMigration).Assembly);
                });
                
                var sp = services.BuildServiceProvider();
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
                        DatabaseHelper.InitialiseDbForTests(scopedServices)
                            .GetAwaiter()
                            .GetResult();
                        
                        AppIdentityDbContextSeed.SeedAsync(userManager, roleManager).GetAwaiter().GetResult();
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

    public static class ServiceCollectionTestExtensions
    {
        public static IServiceCollection ReplaceWithSqlite<TContext>(
            this IServiceCollection services, 
            string connectionString)
            where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            return services;
        }
        
        public static IServiceCollection ReplaceWithInMemory<TContext>(
            this IServiceCollection services, 
            string databaseName,
            InMemoryDatabaseRoot dbRoot)
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
                options.UseInMemoryDatabase(databaseName, dbRoot);
            });

            return services;
        }
    }
}
