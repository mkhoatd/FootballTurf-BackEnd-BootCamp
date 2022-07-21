using Microsoft.AspNetCore.SignalR;
using WebApi.Domain.Common;
using WebApi.Repository.Interface;

namespace WebApi.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly IHubRepository _hubRepository;


        public ConnectionHub(IHubRepository hubRepository)
        {
            _hubRepository = hubRepository;
        }

        public async Task AddToGroupAsync(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync(CommonHub.EventAddGroup, Context.ConnectionId, userName);
        }

        public async Task RemoveConnectionAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync(CommonHub.EventOutGroup, $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}