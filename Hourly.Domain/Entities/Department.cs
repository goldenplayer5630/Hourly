using Hourly.Abstractions.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Entities
{
    public class Department : IDepartment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<IUser> Users { get; init; } = new List<IUser>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void Update(IDepartment updatedDepartment)
        {
            if (updatedDepartment == null)
                throw new ArgumentNullException(nameof(updatedDepartment));
            Name = updatedDepartment.Name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Department name cannot be empty.");
            if (Name.Length > 100)
                throw new ValidationException("Department name cannot exceed 100 characters.");
        }
    }
}
