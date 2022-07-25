using GenericBizRunner;
using WebApi.BusinessLogic.Turfs.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.Turfs
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
