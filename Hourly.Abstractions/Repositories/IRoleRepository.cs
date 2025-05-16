using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetById(Guid roleId);
        Task<IEnumerable<Role>> GetAll();
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task SaveChanges();
    }
}
