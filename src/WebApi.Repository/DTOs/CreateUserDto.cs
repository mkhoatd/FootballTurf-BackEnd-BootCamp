using System.ComponentModel.DataAnnotations;
namespace WebApi.Repository.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string RoomId { get; set; } = string.Empty;
    }
}
