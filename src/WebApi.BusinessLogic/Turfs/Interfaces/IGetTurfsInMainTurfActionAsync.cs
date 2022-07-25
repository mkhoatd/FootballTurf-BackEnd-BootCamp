using GenericBizRunner;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.Turfs.Interfaces;

public interface IGetTurfsInMainTurfActionAsync : IGenericActionAsync<String,List<TurfDto>>
{
}