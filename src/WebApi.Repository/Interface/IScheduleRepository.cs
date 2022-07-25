using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Repository.Interface
{
    public interface IScheduleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="turfId"></param>
        /// <returns></returns>
        
        Task<List<Schedule>> GetAllScheduleByIdTurf(Guid turfId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="turfId"></param>
        /// <returns></returns>
        Task<List<Schedule>> GetScheduleByIdTurfInAMonth(Guid turfId);


        Task<Schedule> GetScheduleByIdFromStartTimeAndEndTime(Guid turfId, DateTime startTime, DateTime endTime);
    }
}
