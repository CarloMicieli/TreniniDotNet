using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Text.Json;
using TreniniDotNet.Web.DependencyInjection;
using TreniniDotNet.Infrastracture.Extensions.DependencyInjection;
using TreniniDotNet.Infrastracture.Persistence;
using System;
using TreniniDotNet.Infrastracture.Persistence.Migrations;
using TreniniDotNet.Infrastracture.Persistence.TypeHandlers;
using TreniniDotNet.Web.Identity;
using TreniniDotNet.Infrastructure.Persistence.Seed;
using Microsoft.Extensions.Logging;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;

namespace TreniniDotNet.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            services.AddHttpContextAccessor();

            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDapper(options =>
            {
                options.UsePostgres(connectionString);
                options.ScanTypeHandlersIn(typeof(GuidTypeHandler).Assembly);
            });

            services.AddMigrations(options =>
            {
                options.UsePostgres(connectionString);
                options.ScanMigrationsIn(typeof(InitialMigration).Assembly);
            });

            services.AddOpenApi();
            services.AddVersioning();

            services.AddUseCases();
            services.AddPresenters();
            services.AddRepositories();

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddSingleton<IGuidSource, GuidSource>();
            services.AddSingleton<IClock>(SystemClock.Instance);

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationIdentityDbContext>("DbHealthCheck");

            services.AddEntityFrameworkIdentity(Configuration);
            services.AddJwtAuthentication(Configuration)
                .AddJwtAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");

                // Run database migration
                var migration = serviceProvider.GetRequiredService<IDatabaseMigration>();
                migration.Up();

                // Seed database with test data
                // CatalogSeed.InitDatabase(serviceProvider, logger).GetAwaiter().GetResult();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHealthChecks("/health");

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}