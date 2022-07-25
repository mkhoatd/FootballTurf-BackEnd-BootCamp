using WebApi.Domain.Entities;
using WebApi.Domain.Enum;

namespace WebApi.Repository.Interface
{
    public interface IHubRepository
    {
        Task<Schedule> CreateAndUpdateScheduleTurf(Guid turfId, ScheduleStatus status, DateTime startTime, DateTime endTime);

    }
}
