using Microsoft.EntityFrameworkCore;
using WebApi.BusinessLogic.Schedules.DTOs;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Persistence;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class HubRepository : IHubRepository
    {
        private readonly AppFootballTurfDbContext _context;
        private readonly IScheduleRepository _scheduleRepository;

        public HubRepository(AppFootballTurfDbContext context, IScheduleRepository scheduleRepository)
        {
            _context = context;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedule?> UpdateScheduleTurf(UpdateScheduleDto updateScheduleDto)
        {
            var schedule = await _context.Schedules.SingleOrDefaultAsync(x => x.Id == updateScheduleDto.ScheduleId);
            if (schedule != null)
            {
                var tmp = (ScheduleStatus)Enum.Parse(typeof(ScheduleStatus), updateScheduleDto.Status, true);
                schedule.Status = tmp;
                await _context.SaveChangesAsync();
                return schedule;
            }
            return null;
        }
    }
}
