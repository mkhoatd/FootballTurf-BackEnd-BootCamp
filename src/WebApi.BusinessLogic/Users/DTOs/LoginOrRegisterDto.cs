using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.BusinessLogic.Users.DTOs;

public class LoginOrRegisterDto
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}