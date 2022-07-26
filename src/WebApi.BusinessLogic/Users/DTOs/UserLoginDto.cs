using System.Text.Json.Serialization;

namespace WebApi.BusinessLogic.Users.DTOs;

public class UserLoginDto
{
    [JsonPropertyName("username")] 
    public string Username { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    
}