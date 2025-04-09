using Hourly.Shared.Models;

namespace Hourly.Abstractions.Repositories
{
    public interface IWorkSessionGitCommitRepository
    {
        Task<WorkSessionGitCommit?> GetById(Guid workSessionId, Guid gitCommitId);
        Task<IEnumerable<WorkSessionGitCommit>> GetAll();
        Task Create(WorkSessionGitCommit workSessionGitCommit);
        Task Update(WorkSessionGitCommit workSessionGitCommit);
        Task Delete(Guid workSessionId, Guid gitCommitId);
    }
}
