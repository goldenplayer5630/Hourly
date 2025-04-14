using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; private set; }

        public Guid? DepartmentId { get; private set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; private set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();

        public ICollection<GitCommit> GitCommits { get; set; } = new List<GitCommit>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AssignToDepartment(Department department)
        {
            if (DepartmentId == department.Id)
            {
                throw new DomainValidationException("User is already assigned to this department.");
            }

            DepartmentId = department.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveFromDepartment()
        {
            if (DepartmentId == null)
            {
                throw new DomainValidationException("User is not assigned to any department.");
            }

            DepartmentId = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignRole(Role role)
        {
            if (RoleId == role.Id)
            {
                throw new DomainValidationException("User already has this role.");
            }

            RoleId = role.Id;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
