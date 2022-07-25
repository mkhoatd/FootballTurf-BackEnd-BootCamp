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

        public async Task<Schedule> CreateAndUpdateScheduleTurf(Guid turfId, ScheduleStatus status, DateTime startTime, DateTime endTime)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.Role == UserRole.Customer);
            var schedule = await _scheduleRepository.GetScheduleByIdFromStartTimeAndEndTime(turfId, startTime, endTime);

            if(schedule == null)
            {
               Schedule newSchedule = new Schedule()
               {
                   Start = startTime,
                   End = endTime,
                   Status = status,
                   TurfId = turfId,
                   CustomerId = customer.Id,
               };
               _context.Schedules.Add(newSchedule);
               
            }
            else
            {
                schedule.Status = status;
            }

            await _context.SaveChangesAsync();
            return schedule;
        }
    }
}
