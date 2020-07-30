using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using Serilog;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.GrpcServices.Catalog.Brands;
using TreniniDotNet.GrpcServices.Catalog.CatalogItems;
using TreniniDotNet.GrpcServices.Catalog.DependencyInjection;
using TreniniDotNet.GrpcServices.Catalog.Railways;
using TreniniDotNet.GrpcServices.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.DependencyInjection;

namespace TreniniDotNet.GrpcServices
{
    public class Startup
    {
        private const int OneMegabyte = 1024 * 1024;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddRepositories(Configuration);

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddSingleton<IGuidSource, GuidSource>();
            services.AddSingleton<IClock>(SystemClock.Instance);

            services.AddCatalogUseCases();
            services.AddCatalogServices();
            services.AddCatalogPresenters();
            services.AddUseCaseInputValidators();

            services.AddGrpc(options =>
            {
                options.MaxReceiveMessageSize = OneMegabyte;
                options.MaxSendMessageSize = OneMegabyte;
                options.EnableDetailedErrors = _environment.IsDevelopment();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcBrandsService>();
                endpoints.MapGrpcService<GrpcCatalogItemsService>();
                endpoints.MapGrpcService<GrpcRailwaysService>();
                endpoints.MapGrpcService<GrpcScalesService>();
            });
        }
    }
}
