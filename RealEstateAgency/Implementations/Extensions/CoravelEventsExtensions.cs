using Coravel;
using Microsoft.AspNetCore.Builder;
using System;
using Coravel.Events.Interfaces;
using RealEstateAgency.NotificationSystem;
using RealEstateAgency.NotificationSystem.NotifySubscribers;
using RealEstateAgency.TaskScheduler;
using Serilog;

namespace RealEstateAgency.Implementations.Extensions
{
    public static class CoravelEventsExtensions
    {
        //https://docs.coravel.net/Events/
        public static void ConfigureCoravelEvents(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;
            var registration = provider.ConfigureEvents();
            registration
                .Register<NotifyUserEvent>()
                .Subscribe<SignalRUserEventSubscriber>()
                .Subscribe<EmailUserEventSubscriber>();
        }
    }
}
