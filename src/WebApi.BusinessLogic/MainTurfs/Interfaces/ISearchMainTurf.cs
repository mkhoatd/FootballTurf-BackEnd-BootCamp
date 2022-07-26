using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.MainTurfs.Interfaces
{
    public interface ISearchMainTurf: IGenericActionAsync<SearchTurfDto, List<MainTurfDto>>
    {

    }
}
