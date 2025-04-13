using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitCommitRepository
    {
        Task<GitCommit?> GetById(Guid gitCommitId);
        Task<IEnumerable<GitCommit>> GetAll();
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid gitCommitId);
        Task SaveChanges();
    }
}
