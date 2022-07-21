using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
