using Coravel;
using Microsoft.AspNetCore.Builder;
using System;
using RealEstateAgency.TaskScheduler;
using Serilog;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class CoravelSchedulerExtensions
    {
        //https://docs.coravel.net/Scheduler/
        public static void ConfigureScheduler(this IApplicationBuilder app,ILogger logger)
        {
            var provider = app.ApplicationServices;
            provider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<NotifierJob>()
                    .EveryMinute();
            }).OnError((err) =>
            {
                logger.Error("Coravel task exception: " + err.ToString());
            });
        }
    }
}
