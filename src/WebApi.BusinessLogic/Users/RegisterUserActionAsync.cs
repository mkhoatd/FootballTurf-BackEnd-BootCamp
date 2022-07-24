using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Repository.Interface;
using GenericBizRunner;

namespace WebApi.BusinessLogic.Users;

public class RegisterUserActionAsync : 
    BizActionStatus, 
    IRegisterUserActionAsync
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RegisterUserActionAsync(IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }



    public async Task<UserLoginDto> BizActionAsync(LoginOrRegisterDto dto)
    {
        if(dto.Username.Length<4 || dto.Username.Length>20)
        {
            AddError("Username must be between 4 and 20 characters",nameof(dto.Username));
        }
        if(dto.Password.Length<4 || dto.Password.Length>20)
        {
            AddError("Password must be between 4 and 20 characters",nameof(dto.Password));
        }

        if (dto.Username.Contains(" "))
        {
            AddError("Username must not contain spaces", nameof(dto.Username));
        }

        if (dto.Password.Contains(" "))
        {
            AddError("Password must not contain spaces", nameof(dto.Password));
        }
        
        var user = new User(dto.Username, dto.Password);
        var isExist = await _userRepository.CheckUserExistAsync(user.Username);
        if(isExist)
        {
            AddError("User already exist",nameof(dto.Username));
        }
        
        if (HasErrors) return null;
        _userRepository.AddUser(user);
        var userLoginDto = new UserLoginDto
        {
            Username = user.Username,
            Token = _tokenService.GenerateJwtToken(user)
        };
        return userLoginDto;
    }
}