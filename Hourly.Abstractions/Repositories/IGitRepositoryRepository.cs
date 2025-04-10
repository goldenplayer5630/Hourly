using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitRepositoryRepository
    {
        Task<GitRepository?> GetById(Guid gitRepositoryId);
        Task<IEnumerable<GitRepository>> GetAll();
        Task Create(GitRepository gitRepository);
        Task Update(GitRepository gitRepository);
        Task Delete(Guid gitRepositoryId);
    }
}
