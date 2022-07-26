using Microsoft.AspNetCore.SignalR;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
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

        public async Task AddToGroupAsync(Guid turfId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, turfId.ToString());
            //await Clients.OthersInGroup(idTurf).SendAsync(CommonHub.EventAddGroup, Context.ConnectionId, userName);
        }

        public async Task RemoveConnectionAsync(string turfId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, turfId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateStatusTurfAsync(Guid scheduleId, ScheduleStatus status)
        {
           var schedule =  _hubRepository.UpdateScheduleTurf(scheduleId, status);
           if (schedule == null)
           {
                return;
           }
           else
           {
                await Clients.Group(schedule.Result.TurfId.ToString()).SendAsync(CommonHub.UpdateSchedule, schedule);
           }
        }
    }
}