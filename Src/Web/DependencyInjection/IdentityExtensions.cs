using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TreniniDotNet.Web.Identity;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class IdentityExtensions
    {
        //public static IServiceCollection AddEntityFrameworkIdentity(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<ApplicationIdentityDbContext>(options =>
        //        options.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));

        //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //        .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
        //        .AddDefaultTokenProviders();

        //    return services;
        //}

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
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

        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
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