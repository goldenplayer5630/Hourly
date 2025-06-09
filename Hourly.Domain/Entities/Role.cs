using Hourly.Abstractions.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Entities
{
    public class Role : IRole
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Permissions { get; set; } // Store as JSON string

        public ICollection<IUser> Users { get; init; } = new List<IUser>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
