using CacheManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Implementations.ActionFilters;
using RealEstateAgency.Shared.BaseModels;
using RealEstateAgency.Shared.Services;
using RealEstateAgency.Shared.Statics;
using Serilog;
using System;
using Microsoft.AspNetCore.Hosting;
using RealEstateAgency.Implementations.ApiImplementations.CreationPatterns.EntityServiceFactories;
using RealEstateAgency.Implementations.ApiImplementations.Services;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Authentication.Contracts;
using RealEstateAgency.Implementations.CreateBasics;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Implementations.Services;
using RealEstateAgency.NotificationSystem.NotifySubscribers;
using RealEstateAgency.NotificationSystem.Signalers;
using RealEstateAgency.TaskScheduler;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterDbDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<RealEstateDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(true);
            });
            return services;
        }

        public static IServiceCollection RegisterDefaultDependencies(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            services.AddTransient<IStartupFilter, InitializeDbStartupFilter>();
            services.AddTransient<NotifierJob>();

            services.AddTransient<EmailUserEventSubscriber>();
            services.AddTransient<SignalRUserEventSubscriber>();

            services.AddScoped<IUpdateSignaler, UpdateSignaler>();

            return services;
        }

        public static IServiceCollection RegisterAppConfigurationSections(this IServiceCollection services)
        {
            services.AddSingleton<ISerilogSetting>(sp =>
                sp.GetRequiredService<IOptions<SerilogSetting>>().Value);

            services.AddSingleton<ICorsSetting>(sp =>
                sp.GetRequiredService<IOptions<CorsSetting>>().Value);

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IFileStatics, FileStatics>();
            services.AddSingleton<IUserGroupProvider, UserGroupProvider>();
            services.AddSingleton<ILanguageProvider, LanguageProvider>();
            services.AddSingleton<IPathProvider, PathProvider>();

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUploadHelperService, UploadHelperService>();
            services.AddScoped<IDownloadHelperService, DownloadHelperService>();

            services.AddScoped<IAppAuthService, AppAuthService>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IRecaptchaService, RecaptchaService>();

            services.AddTransient<IFastHasher, FastHasher>();
            services.AddTransient<IPasswordService, PasswordService>();

            return services;
        }

        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityCreateFactory<>),
                typeof(EntityCreateFactory<>));
            services.AddScoped(typeof(IEntityDeleteFactory<>),
                typeof(EntityDeleteFactory<>));
            services.AddScoped(typeof(IEntityUpdateFactory<>),
                typeof(EntityUpdateFactory<>));
            services.AddScoped(typeof(IEntityGetAllFactory<>),
                typeof(EntityGetAllFactory<>));
            services.AddScoped(typeof(IEntityGetFactory<>),

                typeof(EntityGetFactory<>));
            services.AddScoped(typeof(IEntityService<>),
                typeof(EntityService<>));
            services.AddScoped(typeof(IUserAccessService<>),
                typeof(UserAccessService<>));

            services.AddScoped(typeof(IModelService<,>),
                typeof(ModelService<,>));

            return services;
        }

        public static IServiceCollection RegisterActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ExecutionActionFilter>();
            return services;
        }

        public static IServiceCollection RegisterSecondLevelDbCache(this IServiceCollection services)
        {
            // Add an in-memory cache service provider
            services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));
            services.AddSingleton(typeof(ICacheManagerConfiguration),
                new CacheManager.Core.ConfigurationBuilder()
                    .WithJsonSerializer()
                    .WithMicrosoftMemoryCacheHandle(instanceName: "MemoryCache")
                    .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromMinutes(30))
                    .Build());
            return services;
        }
    }
}
