using Microsoft.AspNetCore.Hosting;
using RealEstateAgency.Shared.BaseModels;
using Serilog;
using System.IO;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class SerilogExtensions
    {
        public static ILogger ConfigureLogger(this ILogger logger, SerilogSetting settings) =>
            new LoggerConfiguration()
                .WriteTo.File(Path.Combine(WebHostDefaults.ContentRootKey, settings.Path, settings.LogFileName),
                    rollingInterval: (RollingInterval)settings.RollingInterval)
                .WriteTo.Console()
                .CreateLogger();
    }
}
