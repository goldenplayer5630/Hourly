using Hourly.Domain.Contracts.Responses.DepartmentResponses;
using Hourly.Domain.Contracts.Responses.GitCommitResponses;
using Hourly.Domain.Contracts.Responses.UserContractResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Contracts.Responses.UserResponses
{
    public class UserResponse : UserSummaryResponse
    {

        [ForeignKey("DepartmentId")]
        public DepartmentSummaryResponse? Department { get; set; }

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        public ICollection<UserContractSummaryResponse> Contracts { get; set; } = new List<UserContractSummaryResponse>();
    }
}
