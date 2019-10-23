using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace RealEstateAgency.Hubs
{
    public static class SignalRUsers
    {
        public static Dictionary<int, HashSet<string>> Users { get; }
            = new Dictionary<int, HashSet<string>>();
    }

    [Authorize]
    public class UsersHub : Hub
    {
        public static string Name => nameof(UsersHub);
        public override async Task OnConnectedAsync()
        {
            AddCurrentConnection();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            RemoveCurrentConnection();
            await base.OnDisconnectedAsync(exception);
        }

        private void RemoveCurrentConnection()
        {
            var userId = GetCurrentUserId();
            if (userId is null) return;

            SignalRUsers.Users.TryGetValue(userId.Value, out var existingUserConnectionIds);
            if(existingUserConnectionIds is null) return;
            existingUserConnectionIds.Remove(Context.ConnectionId);

            if (existingUserConnectionIds.Count == 0)
                SignalRUsers.Users.Remove(userId.Value);
        }

        private void AddCurrentConnection()
        {
            var userId = GetCurrentUserId();
            if(userId is null) return;

            SignalRUsers.Users.TryGetValue(userId.Value, out var existingUserConnectionIds);

            if (existingUserConnectionIds is null)
                existingUserConnectionIds = new HashSet<string>();
            else if 
                (existingUserConnectionIds.Contains(Context.ConnectionId)) return;

            existingUserConnectionIds.Add(Context.ConnectionId);
            SignalRUsers.Users.TryAdd(userId.Value, existingUserConnectionIds);
        }

        private int? GetCurrentUserId()
        {
            if (!Context.User.Identity.IsAuthenticated
                || Context.User.Identity.Name == null
                || string.IsNullOrWhiteSpace(Context.User.Identity.Name))
                return null;

            return int.Parse(Context.User.Identity.Name);
        } 
    }
}
