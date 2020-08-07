using System;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Infrastructure.Identity;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Migrations;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;
using TreniniDotNet.IntegrationTests.Helpers.Data;

namespace TreniniDotNet.IntegrationTests
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public sealed class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
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
                services.ReplaceWithInMemory<ApplicationIdentityDbContext>("IdentityInMemoryDatabase");

                var connectionString = new SqliteConnectionStringBuilder($"Data Source={_contextId}")
                {
                    ForeignKeys = true,
                    Cache = SqliteCacheMode.Private,
                    Mode = SqliteOpenMode.ReadWriteCreate
                }.ToString();

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

                RegisterTypeHandlers();
                services.ReplaceWithInMemoryUnitWork();

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
                        AppIdentityDbContextSeed.SeedAsync(userManager, roleManager).Wait();

                        DatabaseHelper.InitialiseDbForTests(scopedServices)
                            .Wait();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

        private static void RegisterTypeHandlers()
        {
            var assembly = typeof(GuidTypeHandler).Assembly;
            var baseType = typeof(SqlMapper.ITypeHandler);

            var typeHandlers = assembly
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t));

            foreach (var typeHandler in typeHandlers)
            {
                var iTypeHandler = Activator.CreateInstance(typeHandler) as SqlMapper.ITypeHandler;
                var type = typeHandler?.BaseType?.GetGenericArguments()[0];
                if (type != null && iTypeHandler != null)
                {
                    SqlMapper.AddTypeHandler(type, iTypeHandler);
                }
            }
        }
    }
#pragma warning restore CA1063 // Implement IDisposable Correctly

    public static class ServiceCollectionTestExtensions
    {
        public static IServiceCollection ReplaceWithInMemoryUnitWork(this IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IUnitOfWork));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddScoped<IUnitOfWork, InMemoryUnitOfWork>();

            return services;
        }

        public static IServiceCollection ReplaceWithInMemory<TContext>(this IServiceCollection services,
            string databaseName)
            where TContext : DbContext
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add ApplicationDbContext using an in-memory database for testing.
            services.AddDbContext<TContext>(options => { options.UseInMemoryDatabase(databaseName); });

            return services;
        }
    }
}
