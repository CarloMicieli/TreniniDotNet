using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace TreniniDotNet.Web.Infrastructure.DependencyInjection
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("JWT Token",
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            Description = "Copy 'Bearer ' + valid JWT token into field",
                            In = OpenApiSecurityApiKeyLocation.Header
                        }));

                config.PostProcess = document =>
                {
                    document.Info.Title = "Trenini.net API";
                    document.Info.Description = "A web API for model railways collection";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Carlo Micieli",
                        Email = string.Empty,
                        Url = "https://carlomicieli.github.io"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });

            return services;
        }
    }
}