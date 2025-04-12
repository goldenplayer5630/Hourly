using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitRepositoryRepository
    {
        Task<GitRepository?> GetById(Guid gitRepositoryId);
        Task<IEnumerable<GitRepository>> GetAll();
        Task<GitRepository> Create(GitRepository gitRepository);
        Task<GitRepository> AddGitCommit(Guid gitRepositoryId, Guid gitCommitId);
        Task<GitRepository> Update(GitRepository gitRepository);
        Task Delete(Guid gitRepositoryId);
    }
}
