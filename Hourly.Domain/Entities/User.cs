using Hourly.Abstractions.Entities;
using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Entities
{
    public class User : IUser
    {
        protected List<IGitCommit> _gitCommits = new();
        protected List<IUserContract> _userContracts = new();

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public IRole Role { get; private set; }

        public Guid? DepartmentId { get; private set; }

        [ForeignKey("DepartmentId")]
        public IDepartment? Department { get; private set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        [Required]
        public float TVTHourBalance { get; set; }

        public IReadOnlyCollection<IGitCommit> GitCommits => _gitCommits.AsReadOnly();

        public IReadOnlyCollection<IUserContract> Contracts => _userContracts.AsReadOnly();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void Update(IUser updatedUser)
        {
            if (updatedUser == null)
                throw new ArgumentNullException(nameof(updatedUser));
            Name = updatedUser.Name;
            Email = updatedUser.Email;
            GitEmail = updatedUser.GitEmail;
            GitUsername = updatedUser.GitUsername;
            GitAccessToken = updatedUser.GitAccessToken;
            TVTHourBalance = updatedUser.TVTHourBalance;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignToDepartment(IDepartment department)
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

        public void AssignToRole(IRole role)
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
