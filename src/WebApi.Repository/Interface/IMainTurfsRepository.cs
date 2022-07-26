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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<MainTurfDto>> GetMainTurfByIdUserAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<MainTurfDto>> GetAllMainTurfsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<MainTurfDto>> SearchMainTurfAsync(SearchTurfDto searchTurfDto);

    }
}