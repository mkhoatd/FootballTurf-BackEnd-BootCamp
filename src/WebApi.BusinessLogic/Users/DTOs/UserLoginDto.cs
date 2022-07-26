using System.Text.Json.Serialization;
using WebApi.Domain.Enum;

namespace WebApi.BusinessLogic.Users.DTOs;

public class UserLoginDto
{
    [JsonPropertyName("username")] 
    public string Username { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("role")]
    public UserRole Role { get; set; }

}