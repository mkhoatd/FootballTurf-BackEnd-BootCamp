using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Persistence;
using WebApi.Repository.Interface;

namespace WebApi.Repository.Implementation
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppFootballTurfDbContext _context;
        public ScheduleRepository(AppFootballTurfDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllScheduleByIdTurf(Guid turfId)
        {
            var listSchedule = await _context.Schedules.Where(x => x.TurfId == turfId).ToListAsync();
            return listSchedule;
        }

        
        public async Task<List<Schedule>> GetScheduleByIdTurfInAMonth(Guid turfId)
        {
            var addOneMonth = DateTime.Today.AddMonths(1).ToUniversalTime();
            var listSchedule = await _context.Schedules.Include(s => s.Customer)
                .Select(s => new Schedule()
                {
                    Start = s.Start,
                    End = s.End,
                    CustomerId = s.CustomerId,
                    TurfId = s.TurfId,
                    Status = s.Status,
                    Customer = s.Customer
                })
                .Where(x => x.TurfId == turfId && x.Start >= DateTime.Today.ToUniversalTime() && x.End <= addOneMonth).ToListAsync();

            return listSchedule;
        }

        public async Task<Schedule> GetScheduleByIdFromStartTimeAndEndTime(Guid turfId, DateTime startTime, DateTime endTime)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Schedules.SingleOrDefaultAsync(x => x.TurfId == turfId && x.Start >= startTime && x.End <= endTime);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
