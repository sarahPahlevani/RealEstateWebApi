using Microsoft.AspNetCore.SignalR;
using RealEstateAgency.Dtos.Other;
using RealEstateAgency.Hubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAgency.NotificationSystem.Signalers
{
    public class UpdateSignaler : IUpdateSignaler
    {
        private readonly IHubClients _clients;
        private const string MethodName = "update_signal";

        public UpdateSignaler(IHubContext<UsersHub> context)
        {
            _clients = context.Clients;
        }

        public async Task Signal(IEnumerable<int> userIds, string entity) =>
            await SendToUsers(userIds, CreateSignalObject(entity));

        public async Task Signal(IEnumerable<int> userIds, string entity, string message) =>
            await SendToUsers(userIds, CreateSignalObject(entity, message));

        public async Task Signal(int userId, string entity) =>
            await SendToUser(userId, CreateSignalObject(entity));


        public async Task Signal(int userId, string entity, string message) =>
            await SendToUser(userId, CreateSignalObject(entity, message));

        public async Task Signal(string entity) =>
            await BroadcastMessage(CreateSignalObject(entity));

        public async Task Signal(string entity, string message) =>
            await BroadcastMessage(CreateSignalObject(entity, message));

        private async Task BroadcastMessage(UpdateSignalDto dto)
            => await _clients.All.SendAsync(MethodName, dto);

        private UpdateSignalDto CreateSignalObject(string entity, string message = null) =>
            new UpdateSignalDto
            {
                Message = message ?? "New update",
                EntityName = entity,
                EntityCode = entity.ToLower().Trim(),
                On = DateTime.Now
            };

        private async Task SendToUsers(IEnumerable<int> userIds, UpdateSignalDto dto)
        {
            var connections = new List<string>();
            foreach (var userId in userIds)
                if (SignalRUsers.Users.TryGetValue(userId, out var value))
                    connections.AddRange(value);

            await _clients.Clients(connections).SendAsync(MethodName, dto);
        }

        private async Task SendToUser(int userId, UpdateSignalDto dto)
        {
            if (SignalRUsers.Users.TryGetValue(userId, out var connections))
                foreach (var connectionId in connections)
                    await _clients.Client(connectionId).SendAsync(MethodName, dto);
        }
    }

    public interface IUpdateSignaler
    {
        Task Signal(IEnumerable<int> userIds, string entity);

        Task Signal(IEnumerable<int> userIds, string entity, string message);

        Task Signal(int userId, string entity);

        Task Signal(int userId, string entity, string message);

        Task Signal(string entity);

        Task Signal(string entity, string message);
    }
}
