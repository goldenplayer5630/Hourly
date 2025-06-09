using Hourly.Shared.Contracts.Responses.GitRepositoryResponses;
using Hourly.Shared.Contracts.Responses.UserResponses;
using Hourly.Shared.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {
        [ForeignKey("AuthorId")]
        public UserSummaryResponse Author { get; set; } = new UserSummaryResponse();

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepositorySummaryResponse Repository { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();
    }
}
