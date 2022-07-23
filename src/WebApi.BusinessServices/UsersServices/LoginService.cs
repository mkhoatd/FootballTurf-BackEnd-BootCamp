using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using WebApi.BusinessLogic.Users;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Repository.Interface;

namespace WebApi.BusinessServices.UsersServices;

public class LoginService
{
    private readonly ICheckValidUsernameAndPasswordActionAsync _action;
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;

    public LoginService(IUserRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
        _action = new CheckValidUsernameAndPasswordActionAsync(_repository);
    }
    public IImmutableList<ValidationResult> Errors => _action.Errors;

    public async Task<UserLoginDto> LoginAsync(LoginOrRegisterDto dto)
    {
        dto.Username = dto.Username.ToLower();
        var result = await _action.ActionAsync(dto);
        if (_action.HasErrors)
        {
            return null;
        }
        var user = await _repository.GetUserByUsernameAsync(dto.Username);
        var token = _tokenService.GenerateJwtToken(user);
        return new UserLoginDto
        {
            Username = user.Username,
            Token = token
        };
    }
}