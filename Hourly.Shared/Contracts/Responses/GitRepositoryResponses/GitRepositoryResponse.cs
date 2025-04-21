using Hourly.Shared.Contracts.Responses.GitCommitResponses;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}
