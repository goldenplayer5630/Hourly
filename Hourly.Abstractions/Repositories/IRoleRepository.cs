using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IRoleRepository
    {
        Task<IRole?> GetById(Guid IRoleId);
        Task<IEnumerable<IRole>> GetAll();
        Task<IRole> Create(IRole IRole);
        Task<IRole> Update(IRole IRole);
        Task SaveChanges();
    }
}
