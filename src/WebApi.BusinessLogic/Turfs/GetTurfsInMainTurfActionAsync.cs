using GenericBizRunner;
using WebApi.BusinessLogic.Turfs.Interfaces;
using WebApi.Repository.DTOs;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.Turfs;

public class GetTurfsInMainTurfActionAsync : 
    BizActionStatus,
    IGetTurfsInMainTurfActionAsync
{
    private readonly ITurfsRepository _repository;

    public GetTurfsInMainTurfActionAsync(ITurfsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TurfDto>> BizActionAsync(string mainTurfId)
    {
        return await _repository.GetTurfsInMainTurf(mainTurfId);
    }
}