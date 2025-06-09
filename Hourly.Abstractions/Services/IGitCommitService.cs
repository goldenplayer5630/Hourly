using Hourly.Domain.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IGitCommitService
    {
        Task<IEnumerable<GitCommit>> GetAll();
        Task<IEnumerable<GitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate);
        Task<GitCommit> GetById(Guid gitCommitid);
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid gitCommitid);
    }
}
