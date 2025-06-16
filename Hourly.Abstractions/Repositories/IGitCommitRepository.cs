using Hourly.Domain.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitCommitRepository
    {
        Task<GitCommit?> GetById(Guid gitCommitId);
        Task<IEnumerable<GitCommit>> GetAll();
        Task<IEnumerable<GitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate);
        Task<IEnumerable<GitCommit>> GetByIds(IEnumerable<Guid> gitCommitIds);  
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid gitCommitId);
        Task SaveChanges();
    }
}
