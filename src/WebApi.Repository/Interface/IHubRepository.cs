using WebApi.Domain.Entities;
using WebApi.Domain.Enum;

namespace WebApi.Repository.Interface
{
    public interface IHubRepository
    {
        Task<Schedule?> UpdateScheduleTurf(Guid turfId, ScheduleStatus status);

    }
}
