using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApi.Domain.Enum;

namespace WebApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserRole Role { get; set; }
        public string PhoneNumber { get; set; }
        public HashSet<Turf> Turfs { get; set; }
    }
}
