using System.IO;
using Coravel;
using EFSecondLevelCache.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RealEstateAgency.Hubs;
using RealEstateAgency.Implementations.Extensions;
using RealEstateAgency.Implementations.Middleware;
using RealEstateAgency.Shared.BaseModels;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace RealEstateAgency
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public static string ApplicationPath = Directory.GetCurrentDirectory();

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILogger logger, ICorsSetting corsSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
          //  app.UseMiddleware<UserProvider1Middleware>();
            app.UseAuthentication();

            app.UseMiddleware<UserProviderMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API V1"));

            app.UseCors(corsSettings.PolicyName);
            app.ConfigureExceptionHandler(logger, env);
            app.ConfigureScheduler(logger);
            app.ConfigureCoravelEvents();
            app.UseSignalR(routes => routes.MapHub<UsersHub>($"/{UsersHub.Name}"));

            if (!Directory.Exists(Path.Combine(ApplicationPath, @"images")))
                Directory.CreateDirectory(Path.Combine(ApplicationPath, @"images"));
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(ApplicationPath, @"images")),
                RequestPath = new PathString("/images")
            });
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAppSettingsSections(Configuration)
                .ConfigureAppCorsSection(Configuration);

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScheduler();
            services.AddEvents();
            services.AddMailer(Configuration);

            services.AddSignalR(conf =>
            {
                conf.EnableDetailedErrors = true;
            });

            services.AddEFSecondLevelCache();
            services.ConfigureAppAuthentication(Configuration);

            Log.Logger = Log.Logger.ConfigureLogger(
                Configuration.GetSection(nameof(SerilogSetting))
                    .Get<SerilogSetting>());

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "Real Estate API", Version = "v1" }));

            services.RegisterDbDependencies(Configuration)
                .RegisterAppConfigurationSections()
                .RegisterDefaultDependencies()
                .RegisterActionFilters()
                .RegisterServices().RegisterApiServices()
                .RegisterSecondLevelDbCache();
        }
    }
}
 