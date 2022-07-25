using GenericBizRunner;
using WebApi.Repository.DTOs;

namespace WebApi.BusinessLogic.Turfs.Interfaces
{
    public interface IGetAllMainTurfActionAsync : IGenericActionOutOnlyAsync<List<MainTurfDto>>
    {
    }
}
