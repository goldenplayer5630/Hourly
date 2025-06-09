using Hourly.Shared.Contracts.Requests.GitCommitRequests;
using Hourly.Shared.Contracts.Responses.GitCommitResponses;

namespace Hourly.Abstractions.Services
{
    public interface IGitCommitService
    {
        Task<IEnumerable<GitCommitSummaryResponse>> GetAll();
        Task<IEnumerable<GitCommitSummaryResponse>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate);
        Task<GitCommitResponse> GetById(Guid gitCommitid);
        Task<GitCommitResponse> Create(CreateGitCommitRequest gitCommit);
        Task Delete(Guid gitCommitid);
    }
}
