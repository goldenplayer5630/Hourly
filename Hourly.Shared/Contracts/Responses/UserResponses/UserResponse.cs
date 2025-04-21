using Hourly.Shared.Contracts.Responses.DepartmentResponses;
using Hourly.Shared.Contracts.Responses.GitCommitResponses;
using Hourly.Shared.Contracts.Responses.RoleResponses;
using Hourly.Shared.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {
        [ForeignKey("RoleId")]
        public RoleSummaryResponse? Role { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentSummaryResponse? Department { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}
