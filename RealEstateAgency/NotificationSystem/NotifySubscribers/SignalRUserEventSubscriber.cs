using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Events.Interfaces;
using Microsoft.AspNetCore.SignalR;
using RealEstateAgency.Hubs;
using RealEstateAgency.NotificationSystem.NotifyMessages;
using Serilog;

namespace RealEstateAgency.NotificationSystem.NotifySubscribers
{
    public class SignalRUserEventSubscriber : IListener<NotifyUserEvent>
    {
        private readonly IHubClients _clients;

        public SignalRUserEventSubscriber(IHubContext<UsersHub> context)
        {
            _clients = context.Clients;
        }

        public async Task HandleAsync(NotifyUserEvent broadcast)
        {
            if (SignalRUsers.Users.TryGetValue(broadcast.Message.UserId, out var connections))
                foreach (var connectionId in connections)
                    await _clients.Client(connectionId).SendAsync("user_event", broadcast.Message);
        }
    }
}
