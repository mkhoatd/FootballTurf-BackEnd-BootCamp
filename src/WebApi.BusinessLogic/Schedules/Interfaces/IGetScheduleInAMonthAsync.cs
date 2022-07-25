using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Schedules.DTOs;
using WebApi.Domain.Entities;

namespace WebApi.BusinessLogic.Schedules.Interfaces
{
    public interface IGetScheduleInAMonthAsync: IGenericActionAsync<GetScheduleDto,List<Schedule>>
    {
    }
}
