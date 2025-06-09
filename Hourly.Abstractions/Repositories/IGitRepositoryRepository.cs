using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitRepositoryRepository
    {
        Task<IGitRepository?> GetById(Guid IGitRepositoryId);
        Task<IEnumerable<IGitRepository>> GetAll();
        Task<IGitRepository> Create(IGitRepository IGitRepository);
        Task<IGitRepository> Update(IGitRepository IGitRepository);
        Task Delete(Guid IGitRepositoryId);
        Task SaveChanges();
    }
}
