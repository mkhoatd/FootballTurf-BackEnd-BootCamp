using System.Security.Cryptography;
using System.Text;
using GenericBizRunner;
using WebApi.BusinessLogic.Users.DTOs;
using WebApi.BusinessLogic.Users.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Repository.Interface;

namespace WebApi.BusinessLogic.Users;

public class LoginActionAsync : 
    BizActionStatus, 
    ILoginActionAsync
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginActionAsync(IUserRepository userRepository,
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
        var isExist = await _userRepository.CheckUserExistAsync(dto.Username);
        if(!isExist)
        {
            AddError("User doesn't exist",nameof(dto.Username));
            return null;
        }
        var user = await _userRepository.GetUserByUsernameAsync(dto.Username);
        using var hmac=new HMACSHA512(user.PasswordSalt);
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
        if (!passwordHash.SequenceEqual(user.PasswordHash))
        {
            AddError("Wrong password", nameof(dto.Password));
        }
        if (HasErrors) return null;
        var userLoginDto = new UserLoginDto
        {
            Username = user.Username,
            Token = _tokenService.GenerateJwtToken(user)
        };
        return userLoginDto;
    }
}