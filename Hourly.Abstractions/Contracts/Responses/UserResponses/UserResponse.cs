using Hourly.Shared.Entities;
using Hourly.Abstractions.Contracts.Responses.DepartmentResponses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Contracts.Responses.RoleResponses;
using Hourly.Abstractions.Contracts.Responses.WorkSessionResponses;
using Hourly.Abstractions.Contracts.Responses.GitCommitResponses;

namespace Hourly.Abstractions.Contracts.Responses.UserResponses
{
    public class UserResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public Guid? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public RoleSummaryResponse? Role { get; set; }

        public Guid? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentSummaryResponse? Department { get; set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
