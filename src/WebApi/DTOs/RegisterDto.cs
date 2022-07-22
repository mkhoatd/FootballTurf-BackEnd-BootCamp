using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.DTOs;

public class RegisterDto
{
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
}