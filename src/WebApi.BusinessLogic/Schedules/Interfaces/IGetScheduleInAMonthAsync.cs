using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.Schedules.Interfaces
{
    public interface IGetScheduleInAMonthAsync : IGenericActionAsync<Guid, List<ScheduleDto>>
    {
    }
}
