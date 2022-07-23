using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using WebApi.BusinessLogic.Users;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Repository.Interface;

namespace WebApi.BusinessServices.UsersServices;

public class RegisterUserService
{
    private readonly IRegisterUserActionAsync _action;
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;

    public RegisterUserService(
        IUserRepository repository,
        ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
        _action = new RegisterUserActionAsync(repository);
    }

    public IImmutableList<ValidationResult> Errors => _action.Errors;
    public async Task<UserLoginDto> RegisterAsync(LoginOrRegisterDto dto)
    {
        dto.Username = dto.Username.ToLower();
        var user = await _action.ActionAsync(dto);
        if (_action.HasErrors)
        {
            return null;
        }

        _repository.AddUser(user);
        await _repository.SaveChangesAsync();
        var userLoginDto = new UserLoginDto
        {
            Username = user.Username,
            Token = _tokenService.GenerateJwtToken(user)
        };
        return userLoginDto;
    }
}