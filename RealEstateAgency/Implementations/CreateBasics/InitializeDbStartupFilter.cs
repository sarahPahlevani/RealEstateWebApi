using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Shared.Services;
using System;
using RealEstateAgency.Implementations.Authentication.Contracts;

namespace RealEstateAgency.Implementations.CreateBasics
{
    public class InitializeDbStartupFilter : IStartupFilter
    {
        //https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
        private readonly IServiceProvider _serviceProvider;

        public InitializeDbStartupFilter(IServiceProvider serviceProvider,
            // warm ups
            IUserGroupProvider groupProvider)
            => _serviceProvider = serviceProvider;

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RealEstateDbContext>();
                var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();
                new Initializer(context, passwordService).Initialize().GetAwaiter().GetResult();
            }
            return next;
        }
    }
}
