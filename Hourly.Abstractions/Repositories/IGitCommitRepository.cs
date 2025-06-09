using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitCommitRepository
    {
        Task<IGitCommit?> GetById(Guid IGitCommitId);
        Task<IEnumerable<IGitCommit>> GetAll();
        Task<IEnumerable<IGitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate);
        Task<IGitCommit> Create(IGitCommit IGitCommit);
        Task Delete(Guid IGitCommitId);
        Task SaveChanges();
    }
}
