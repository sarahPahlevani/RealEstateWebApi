using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.Shared.BaseModels;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RealEstateAgency.Hubs;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAppSettingsSections(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SerilogSetting>(
                configuration.GetSection(nameof(SerilogSetting)));

            services.Configure<AuthSetting>(
                configuration.GetSection(nameof(AuthSetting)));

            services.Configure<AppSetting>(
                configuration.GetSection(nameof(AppSetting)));

            services.Configure<RecaptchaSetting>(
                configuration.GetSection(nameof(RecaptchaSetting)));

            return services;
        }

        public static IServiceCollection ConfigureAppCorsSection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<CorsSetting>(
                configuration.GetSection(nameof(CorsSetting)));

            if (!(configuration.GetSection(nameof(CorsSetting))
                .Get(typeof(CorsSetting)) is CorsSetting corsSetting)) throw new ArgumentNullException(nameof(corsSetting));

            services.AddCors(options =>
            {
                options.AddPolicy(corsSetting.PolicyName,
                    builder =>
                    {
                        var origins = corsSetting.AllowOrigins.ToArray();
                        builder.WithOrigins(origins).AllowAnyHeader()
                            .AllowAnyMethod().AllowCredentials();
                    });
            });

            return services;
        }

        public static IServiceCollection ConfigureAppAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {

            if (!(configuration.GetSection(nameof(AuthSetting))
                .Get(typeof(AuthSetting)) is AuthSetting authSettings))
                throw new ArgumentNullException(nameof(AuthSetting));

            var key = Encoding.ASCII.GetBytes(authSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        LifetimeValidator = (before, expires, token, param) 
                            => expires > DateTime.Now,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    //for signal r
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments($"/{UsersHub.Name}")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
    }
}
