using WebApi.BusinessLogic.Interfaces;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.Domain.Entities;

namespace WebApi.BusinessLogic.Users.Interfaces;

public interface ICheckValidUsernameAndPasswordActionAsync : IBusinessActionAsync<LoginOrRegisterDto, bool>
{
    
}