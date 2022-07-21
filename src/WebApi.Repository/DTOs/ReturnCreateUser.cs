using System.ComponentModel.DataAnnotations;

namespace WebApi.Repository.Dtos
{
    public class ReturnCreateUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
