using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Entities
{
    public class Department
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
