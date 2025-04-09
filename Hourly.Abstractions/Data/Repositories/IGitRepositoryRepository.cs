using Hourly.Shared.Models;

namespace Hourly.Abstractions.Data.Repositories
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
