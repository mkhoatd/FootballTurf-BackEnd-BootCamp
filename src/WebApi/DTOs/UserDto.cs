using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.DTOs;

public class UserDto
{
    [Required]
    [StringLength(20, MinimumLength = 4)]
    [JsonProperty("username")]
    public string Username { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 8)]
    [JsonProperty("token")]
    public string Token { get; set; }
}