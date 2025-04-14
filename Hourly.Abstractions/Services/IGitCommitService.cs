using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IGitCommitService
    {
        Task<IEnumerable<GitCommit>> GetAll();
        Task<GitCommit> GetById(Guid gitCommitid);
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid gitCommitid);
    }
}
