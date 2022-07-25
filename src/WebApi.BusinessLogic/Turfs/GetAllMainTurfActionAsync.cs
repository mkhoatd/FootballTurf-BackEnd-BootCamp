using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBizRunner;
using WebApi.BusinessLogic.Turfs.Interfaces;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.Turfs
{
    public class GetAllMainTurfActionAsync :
        BizActionStatus,
        IGetAllMainTurfActionAsync
    {
        public async Task<List<MainTurfDto>> BizActionAsync()
        {
            return null;
        }
    }
}
