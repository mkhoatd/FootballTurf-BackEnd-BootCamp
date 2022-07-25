using GenericBizRunner;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.MainTurfs.Interfaces
{
    public interface IGetAllMainTurfActionAsync : IGenericActionOutOnlyAsync<List<MainTurfDto>>
    {
    }
}
