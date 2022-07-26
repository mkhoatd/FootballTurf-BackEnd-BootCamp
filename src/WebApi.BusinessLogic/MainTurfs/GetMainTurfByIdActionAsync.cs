using GenericBizRunner;
using WebApi.BusinessLogic.MainTurfs.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.MainTurfs;

public class GetMainTurfByIdActionAsync :
    BizActionStatus,
    IGetMainTurfByIdActionAsync
{
    private readonly IMainTurfsRepository _repository;

    public GetMainTurfByIdActionAsync(IMainTurfsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MainTurfDto>> BizActionAsync(Guid id)
    {
        return await _repository.GetMainTurfByIdUserAsync(id);
    }
}