using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Schedules.DTOs;
using WebApi.BusinessLogic.Schedules.Interfaces;
using WebApi.Domain.Common;
using WebApi.Domain.Entities;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.Schedules
{
    public class GetScheduleInAMonthAsync: BizActionStatus, IGetScheduleInAMonthAsync
    {
        private readonly IScheduleRepository _scheduleRepository;

        public GetScheduleInAMonthAsync(IScheduleRepository userRepository)
        {
            _scheduleRepository = userRepository;
        }



        public async Task<List<Schedule>> BizActionAsync(GetScheduleDto dto)
        {
            var listSchedule = await _scheduleRepository.GetScheduleByIdTurfInAMonth(dto.TurfId);
            if (listSchedule == null)
                AddError(ExceptionMessage.GetScheduleInAMonthFail, nameof(dto.TurfId));

            return listSchedule;
        }
    }
}
