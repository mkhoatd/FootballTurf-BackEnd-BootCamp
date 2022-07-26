using GenericBizRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BusinessLogic.MainTurfs.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.MainTurfs
{
    public class SearchMainTurfActionAsync :
    BizActionStatus,
    ISearchMainTurf
    {
        private readonly IMainTurfsRepository _repository;

        public SearchMainTurfActionAsync(IMainTurfsRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MainTurfDto>> BizActionAsync(SearchTurfDto inputData)
        {
            return await _repository.SearchMainTurfAsync(inputData);
        }
    }
}
