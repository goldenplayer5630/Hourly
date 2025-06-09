using Hourly.Shared.Contracts.Requests.GitRepositoryRequests;
using Hourly.Shared.Contracts.Responses.GitRepositoryResponses;

namespace Hourly.Abstractions.Services
{
    public interface IGitRepositoryService
    {
        Task<IEnumerable<GitRepositorySummaryResponse>> GetAll();
        Task<GitRepositoryResponse> GetById(Guid id);
        Task<GitRepositoryResponse> Create(CreateGitRepositoryRequest gitRepository);
        Task<GitRepositoryResponse> Update(Guid id, UpdateGitRepositoryRequest gitRepository);
        Task Delete(Guid id);
    }
}
