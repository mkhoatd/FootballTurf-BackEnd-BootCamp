using GenericBizRunner;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.MainTurfs.Interfaces;

public interface IGetMainTurfByIdActionAsync : IGenericActionAsync<String, MainTurfDto>
{
    
}