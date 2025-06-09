using Hourly.Domain.Contracts.Responses.GitCommitResponses;

namespace Hourly.Domain.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse : GitRepositorySummaryResponse
    {
        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();
    }
}
