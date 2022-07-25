using GenericBizRunner;
using WebApi.BusinessLogic.MainTurfs.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.Repository.Implementation;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.MainTurfs
{
    public class GetAllMainTurfActionAsync :
        BizActionStatus,
        IGetAllMainTurfActionAsync
    {
        private readonly IMainTurfsRepository _repo;

        public GetAllMainTurfActionAsync(IMainTurfsRepository _repo)
        {
            this._repo = _repo;
        }
        public async Task<List<MainTurfDto>> BizActionAsync()
        {
            return await _repo.GetAllMainTurfsAsync();
        }
    }
}
