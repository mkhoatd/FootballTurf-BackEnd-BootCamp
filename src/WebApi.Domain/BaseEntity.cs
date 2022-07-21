using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain
{
    public class BaseEntity : BaseTime
    {
        [Key]
        public Guid Id { get; set; }
    }
}
