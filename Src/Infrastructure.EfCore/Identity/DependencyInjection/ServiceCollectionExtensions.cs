using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TreniniDotNet.Infrastructure.Persistence;

namespace TreniniDotNet.Infrastructure.Identity.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkIdentity(configuration);

            services.AddJwtAuthentication(configuration)
                .AddJwtAuthorization();

            return services;
        }

        private static IServiceCollection AddEntityFrameworkIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfigSection = configuration.GetSection("JWT");
            services.Configure<JwtSettings>(jwtConfigSection);

            JwtSettings jwtSettings = jwtConfigSection.Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            return services;
        }

        private static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
            /*
                        services.AddAuthorization(options =>
                        {
                            options.AddPolicy("EditPolicy", policy =>
                                policy.Requirements.Add(new SameOwnerRequirement()));
                        });
            */
            //          services.AddSingleton<IAuthorizationHandler, CollectionAuthorizationHandler>();
            //        services.AddSingleton<IAuthorizationHandler, CollectionAuthorizationCrudHandler>();
            services.AddSingleton<ITokensService, JwtTokensService>();

            return services;
        }
    }
}
