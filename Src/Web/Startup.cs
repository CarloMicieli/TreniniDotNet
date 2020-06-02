using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text.Json;
using System;
using NodaTime;
using AutoMapper;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Infrastructure.Persistence.TypeHandlers;
using TreniniDotNet.Infrastructure.Persistence.Migrations;
using TreniniDotNet.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TreniniDotNet.Application.Catalog;
using TreniniDotNet.Application.Collecting;
using TreniniDotNet.Web.Catalog.V1;
using TreniniDotNet.Web.Collecting.V1;
using TreniniDotNet.Web.Infrastructure.DependencyInjection;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Uploads;
using TreniniDotNet.Web.UserProfiles.Identity;

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

            services.AddControllers(o =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    o.Filters.Add(new AuthorizeFilter(policy));
                })
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

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddSingleton<ILinksGenerator, AspNetLinksGenerator>();

            services.AddCatalogUseCases();
            services.AddCatalogPresenters();

            services.AddCollectingUseCases();
            services.AddCollectingPresenter();

            services.AddRepositories();

            services.AddSingleton<IGuidSource, GuidSource>();
            services.AddSingleton<IClock>(SystemClock.Instance);

            var uploadsSection = Configuration.GetSection("Uploads");
            services.Configure<UploadSettings>(uploadsSection);

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationIdentityDbContext>("DbHealthCheck");

            services.AddEntityFrameworkIdentity(Configuration);

            services.AddJwtAuthentication(Configuration)
                .AddJwtAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILogger<Startup> logger)
        {
            if (env.IsDevelopmentOrTesting())
            {
                app.UseExceptionHandler("/error-local-development");

                // Run database migration
                var migration = serviceProvider.GetRequiredService<IDatabaseMigration>();
                migration.Up();
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
            app.UseSwaggerUi3(settings =>
            {
                settings.TagsSorter = "alpha";
                settings.OperationsSorter = "alpha";
            });

            app.UseHealthChecks("/health");

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "UI";

                if (env.IsDevelopmentOrTesting())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }

    public static class HostEnvironmentEnvExtensions
    {
        public static bool IsDevelopmentOrTesting(this IHostEnvironment hostEnvironment) =>
            hostEnvironment.IsDevelopment() || "Testing" == hostEnvironment.EnvironmentName;
    }
}
