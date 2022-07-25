using WebApi.Repository.DTOs;

namespace WebApi.Repository.Interface;

public interface ITurfsRepository
{
    Task<List<TurfDto>> GetTurfsInMainTurf(string mainTurfId);
}

