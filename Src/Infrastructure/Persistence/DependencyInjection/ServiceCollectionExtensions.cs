using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Persistence.Migrations;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.CatalogItems;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shared;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shops;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Persistence.Repositories.Images;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;

namespace TreniniDotNet.Infrastructure.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            return services.AddMigrations(options =>
            {
                options.UsePostgres(connectionString);
                options.ScanMigrationsIn(typeof(InitialMigration).Assembly);
            });
        }

        public static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<IDatabaseMigration>>();
            logger.LogInformation("Running database migration");

            // REMARKS: IDatabaseMigration is scoped, without an actual request we need to fake one
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var migration = scope.ServiceProvider.GetRequiredService<IDatabaseMigration>();
            migration.Up();

            return app;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDapper(options =>
            {
                options.UsePostgres(connectionString);
                options.ScanTypeHandlersIn(typeof(GuidTypeHandler).Assembly);
            });

            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<ICatalogItemsRepository, CatalogItemsRepository>();
            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<ICollectionsRepository, CollectionsRepository>();
            services.AddScoped<IShopsRepository, ShopsRepository>();
            services.AddScoped<IWishlistsRepository, WishlistsRepository>();
            services.AddScoped<ICatalogItemRefsRepository, CatalogItemRefsRepository>();
            services.AddScoped<ImagesRepository>();
            return services;
        }
    }
}