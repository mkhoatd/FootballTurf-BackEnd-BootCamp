using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Schedule?> UpdateScheduleTurf(Guid scheduleId, ScheduleStatus status)
        {
            var schedule = await _context.Schedules.SingleOrDefaultAsync(x => x.Id == scheduleId);
            if (schedule != null)
            {
                schedule.Status = status;
                await _context.SaveChangesAsync();
                return schedule;
            }
            return null;
        }
    }
}
