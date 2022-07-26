using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Repository.DTOs;

namespace WebApi.Repository.Interface
{
    public interface IMainTurfsRepository
    {
        Task<List<MainTurfDto>> GetMainTurfByIdUserAsync(Guid id);
        Task<List<MainTurfDto>> GetAllMainTurfsAsync();
    }
}
