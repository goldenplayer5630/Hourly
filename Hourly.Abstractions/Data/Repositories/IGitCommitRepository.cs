using Hourly.Shared.Models;

namespace Hourly.Abstractions.Data.Repositories
{
    public interface IGitCommitRepository
    {
        Task<GitCommit?> GetById(Guid gitCommitId);
        Task<IEnumerable<GitCommit>> GetAll();
        Task Create(GitCommit gitCommit);
        Task Update(GitCommit gitCommit);
        Task Delete(Guid gitCommitId);
    }
}
