using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBizRunner;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.Turfs.Interfaces
{
    public interface IGetAllMainTurfActionAsync : IGenericActionOutOnlyAsync<List<MainTurfDto>>
    {
    }
}
