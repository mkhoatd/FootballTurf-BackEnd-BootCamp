using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Repository.DTOs;
using WebApi.Domain.Entities;

namespace WebApi.BusinessLogic.Schedules.Interfaces
{
    public interface IGetScheduleInAMonthAsync : IGenericActionAsync<Guid, List<Schedule>>
    {
    }
}
