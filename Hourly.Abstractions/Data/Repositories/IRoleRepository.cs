using Hourly.Shared.Models;

namespace Hourly.Abstractions.Data.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetById(Guid roleId);
        Task<IEnumerable<Role>> GetAll();
        Task Create(Role role);
        Task Update(Role role);
        Task Delete(Guid roleId);
    }
}
