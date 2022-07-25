using GenericBizRunner;
using WebApi.BusinessLogic.Turfs.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;


namespace WebApi.BusinessLogic.Turfs;

public class GetMainTurfByIdActionAsync :
    BizActionStatus,
    IGetMainTurfByIdActionAsync
{
    private readonly IMainTurfsRepository _repository;

    public GetMainTurfByIdActionAsync(IMainTurfsRepository repository)
    {
        _repository = repository;
    }

    public async Task<MainTurfDto> BizActionAsync(string id)
    {
        return await _repository.GetMainTurfByIdAsync(id);
    }
}