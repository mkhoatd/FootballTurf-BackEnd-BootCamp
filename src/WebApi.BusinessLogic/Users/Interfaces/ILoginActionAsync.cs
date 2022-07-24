using GenericBizRunner;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.Domain.Entities;

namespace WebApi.BusinessLogic.Users.Interfaces;

public interface ILoginActionAsync : IGenericActionAsync<LoginOrRegisterDto, UserLoginDto>
{
    
}