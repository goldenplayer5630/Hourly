using Hourly.Shared.Contracts.Responses.GitCommitResponses;
using Hourly.Shared.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse : WorkSessionSummaryResponse
    {
        [ForeignKey("UserId")]
        public UserSummaryResponse? User { get; set; }

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
